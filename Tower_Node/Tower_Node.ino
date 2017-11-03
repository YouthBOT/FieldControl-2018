//YBOT Tower Node Code
//Uses Neo-Pixel for LEDS
//Uses Audino CAN-Shield for CAN-BUS
//1-wire for relay control

#include <mcp_can.h>
#include <SPI.h>
#include <string.h>
#include <Adafruit_NeoPixel.h>
#include <EEPROM.h>


////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary> NeoPixel Variables </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma region NeoPixel Variables
//Neo Pixel data pin
uint8_t neoPixPin = 8;
//Total number for Neo Pixels in the light
uint8_t stripLength = 32;
//Pixels per ring
uint8_t pixPerRing = 8;
//Defines how bright each pixel should be.
uint8_t brt = 50;

//Start NeoPixel library
Adafruit_NeoPixel light = Adafruit_NeoPixel(stripLength, neoPixPin, NEO_GRB + NEO_KHZ800);

//Define Colors
uint32_t red = light.Color(brt, 0, 0);
uint32_t green = light.Color(0, brt, 0);
uint32_t blue = light.Color(0, 0, brt);
uint32_t white = light.Color(brt, brt, brt);
uint32_t yellow = light.Color(brt, brt, 0);
uint32_t off = light.Color(0, 0, 0);
#pragma endregion

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary> CanBus Variables </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma region CANBUS Variables
//Can-bus pin (Uno Shield pin 9 ; Leonardo Boards pin 17)
uint8_t canPin = 17;
//Can-bus Interrupt Pin (Uno Shield 0 ; Leonardo 4)
uint8_t canIntrPin = 4;
//Tower Node Number
//uint32_t nodeID = 10;
uint32_t nodeID = (uint32_t)EEPROM.read(0);
//Default Team's color is set automatically
uint32_t defaultColor = red;
//Red Side = 21 Green Side = 22 set automatically
uint32_t defaultSide = 21;
//Command Node Number
uint32_t commandNode = 31;

// Set CanBus pin
MCP_CAN CAN(canPin);
//Can-bus message
byte canOut[8] = { 0, 0, 0, 0, 0, 0, 0, 0 };
//Interrupt flag
uint8_t canRecv = 0;
//Length of can-bus message
uint8_t canLength = 0;
//Read Buffer
byte canIn[8] = { 0, 0, 0, 0, 0, 0, 0, 0 };
//Destination Node
uint8_t destNode = 0;
//Tower Node status 
byte nodeStatus[8] = { 5, 0, 0, 0, 0, 0, 0, 0 };

#pragma endregion

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary> IO Variables </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma region IO Variables
//Input Pin number - Pin# of Filtered Digital Inputs - 99 if not used
uint8_t inputPins[6] = { 11, 10, 99, 99, 99, 99 };	//Green 10, Red 11
//Output Pin number - Pin# of Output - 99 if not used
uint8_t outputPins[6] = { 99, 99, 99, 99, 99, 99 };
//LED Pin number (Uno 13, Leonardo 23)
uint8_t ledPin = 23;
//Button State of the Filtered Digital Inputs - 0 = open; 1 = closed
byte inputStates[6] = { 0, 0, 0, 0, 0, 0 };
byte outputState[6] = { 0, 0, 0, 0, 0, 0 };
uint8_t autoPin = 5;
uint8_t manPin = 6;
uint8_t manTonPin = 4;
int manTonState = 1;

#pragma endregion

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary> Program Flags Variables </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
boolean towerSelected = false;		//If the tower is selected
uint8_t selectedState = 0;			//Selected state of tower
boolean gameModeChanged = true;		//If mode is changed
boolean complete = false;			//If task is complete
uint8_t function = 0;				//Function Type
uint8_t functionMode = 0;			//Fuction Mode
uint8_t gameMode = 8;				//Game Mode Value - starts in Debug mode
uint8_t delayMultiplier = 0;		//Report Delay Multiplier
int messagesSent = 0;
int messagesRecieved = 0;

boolean sunState = false;			//Sun's state True = on, False = off
boolean alarmState = false;			//Alarm state True = on, False = off
boolean testedState = false;		//Tower's teseted state True = tested, False = not tested


////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method> Ardiuno Setup Method</method>
////////////////////////////////////////////////////////////////////////////////////////////////////
void setup()
{
	int d = nodeID * 100; //stagger startup time
	Serial.begin(115200);	//Start serial communication
	delay(1000 + d); // Wait for power
	Serial.print("Node ID: ");
	Serial.println(nodeID);

	randomSeed(analogRead(1));

	defaultColor = getDefaultColor();
	if (defaultColor == red) defaultSide = 21;
	else defaultSide = 22;

	//Setup up and turn on the onboard LED to indicated power
	pinMode(ledPin, OUTPUT);
	digitalWrite(ledPin, HIGH);
	//Set up Auto and Manual Pins
	if ((nodeID == 3) || (nodeID == 8))
	{
		pinMode(autoPin, OUTPUT);
		pinMode(manPin, OUTPUT);
		digitalWrite(autoPin, LOW);
		digitalWrite(manPin, LOW);
		pinMode(manTonPin, INPUT_PULLUP);

	}

	//Setup NeoPixels and test them
	Serial.println("Neopixel Test");
	light.begin();						//Start NeoPixel light
	light.show();						// Initialize all pixels to 'off'
	testPattern();						//Test Neopixels
	solidColor(red, 0, 0, stripLength); //Set all red to begin self check

	//Setup up Inputs and test to see if they are in desired state
	Serial.println("-----Inputs----");
	for (int i = 0; i < sizeof(inputPins); i++)
	{
		//If pin is a valid pin
		if (inputPins[i] != 99)
		{
			// Setup the button with an internal pull-up :
			pinMode(inputPins[i], INPUT_PULLUP);

			//Print the input state for each button
			Serial.print("Pin#");
			Serial.print(inputPins[i]);
			Serial.print(" = ");
			Serial.print(checkInput(i));

			if (!checkInput(i))
			{
				wipeColor(green, 0, 0, firstPixel(1), lastPixel(1));	//If okay light ring green
				Serial.println(" - OK");

			}
			else
			{
				wipeColor(yellow, 0, 0, firstPixel(1), lastPixel(1));	//If not light ring yellow
				Serial.println(" - PRESSED");
			}
			delay(250);	//Delay so we can see the result
		}

	}


	//Start CAN BUS
	Serial.println("-----Can Bus----");
START_INIT:

	Serial.print("CAN-BUS Startup: ");
	//If CANBUS begins 
	if (CAN_OK == CAN.begin(CAN_50KBPS))						// init can bus : baudrate = 50k
	{
		wipeColor(green, 0, 0, firstPixel(2), lastPixel(2));	//If okay light ring green
		Serial.println("OK");									//Report Okay

	}
	else
	{
		Serial.println("Not Good, check your pin# and connections");	//Report a problem
		wipeColor(yellow, 0, 0, firstPixel(2), lastPixel(2));			//If not okay light ring yellow
		goto START_INIT;												//If startup failed try again
	}

	attachInterrupt(canIntrPin, MCP2515_ISR, FALLING); // start interrupt for can-bus

	// There are 2 mask in mcp2515
	// Set both Masks
	CAN.init_Mask(0, 0, 0x1f);
	CAN.init_Mask(1, 0, 0x1f);

	// set filter, we can receive id
	CAN.init_Filt(0, 0, nodeID);                          // there are 6 filter in mcp2515
	CAN.init_Filt(1, 0, 0x00);                            // there are 6 filter in mcp2515

	CAN.init_Filt(2, 0, nodeID);                          // there are 6 filter in mcp2515
	CAN.init_Filt(3, 0, 0x00);							  // there are 6 filter in mcp2515
	CAN.init_Filt(4, 0, defaultSide);                          // there are 6 filter in mcp2515
	CAN.init_Filt(5, 0, nodeID);                          // there are 6 filter in mcp2515

														  //Check to see if we can talk to the command node
	delay(1000 * nodeID);								//Delay so all nodes don't talk at the same time				
	uint32_t dA = address(commandNode, nodeID);		//create an unique address
	CAN.sendMsgBuf(dA, 0, sizeof(canOut), canOut, 2); 	//Send a blank message
	delay(100);										//Give the command node time to send it back

	if (canRecv)// check if get data
	{
		canRecv = 0;                   // clear flag

									   // iterate over all pending messages
									   // If either the bus is saturated or the MCU is busy,
									   // both RX buffers may be in use and reading a single
									   // message does not clear the IRQ condition.
		while (CAN_MSGAVAIL == CAN.checkReceive())
		{
			// read data,  canLength: data length, canIn: data incoming
			CAN.readMsgBuf(&canLength, canIn);
		}
		wipeColor(green, 0, 0, firstPixel(3), lastPixel(3));	//If okay light ring green
		Serial.println("Message Received");
	}
	else
	{
		wipeColor(yellow, 0, 0, firstPixel(3), lastPixel(3));  //If not okay light ring yellow
		Serial.println("Nothing Received");
	}

	delay(10000); //Visual Delay

	wipeColor(green, 0, 0, firstPixel(4), lastPixel(4));	//If okay light ring green

	delay(500);

	solidColor(off, 0, 0, stripLength);	//Turn off light



}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method> Ardiuno Main Loop Method</method>
////////////////////////////////////////////////////////////////////////////////////////////////////
void loop()
{
	//While there is data in the buffers
	while (CAN_MSGAVAIL == CAN.checkReceive())
	{
		// read data,  canLength: data length, canIn: data incoming
		CAN.readMsgBuf(&canLength, canIn);
		messagesRecieved++;
		//Process incoming data
		execute();
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////
	/// Main Game Code Bellow
	////////////////////////////////////////////////////////////////////////////////////////////////////
	if (function == 8)
	{
		if (functionMode == 1) gamePlayRandomTest();
		else if (functionMode == 2) gamePlaySpeedTest();
	}
	if (function == 9)
	{
		if (functionMode == 1) NetworkResponseTest();
		else if (functionMode == 2) NetworkSpeedTest();
	}
	else gamePlayCanbus();

	//Watch for Player station button
	if (gameMode == 3)
	{
		if ((nodeID == 3) || (nodeID == 8))
		{
			int oldState = manTonState;
			manTonState = digitalRead(manTonPin);

			//If Pressed Start Mantonomous Mode for that side of the field
			if (oldState != manTonState)
			{
				Serial.println(manTonState);

				if (manTonState == 0)
				{
					nodeStatus[3] = 4;
					report(0, commandNode);
					//StartManTon();
					gameModeChanged = true;
					gameMode = 4;
				}
			}
		}
	}
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>Mcp2515isr()</method>
///
/// <summary>	YBOT, 11/20/2015. </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
void MCP2515_ISR()
{
	canRecv = 1;	//Set Receive Flag to one when interrupt is triggered
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>getDefaultColor()</method>
///
/// <summary>	returns the color of the tower </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
uint32_t getDefaultColor()
{
	uint32_t color;
	if (nodeID < 6) color = red;
	else color = green;

	return color;
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>Execute()</method>
///
/// <summary>Used to execute incoming commands</summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
void execute()
{
	uint_least8_t msgType = canIn[0];		//First byte = Message type

	if (msgType == 0)						//Report nodeStatus
	{
		report(true, sendingNode());		//Send Report to the sending Node
	}
	else if (msgType == 1)					//Set neopixels to desired state
	{
		neoPix();
	}
	else if (msgType == 2)					//Set current game mode
	{
		gameMode = canIn[1];				//Set Game Mode to the new Game Mode
		gameModeChanged = true;
		nodeStatus[3] = gameMode;
	}
	else if (msgType == 3)					//Set transmitters to desired state
	{
		if (canIn[1] == 0)
		{
			digitalWrite(autoPin, LOW);
			digitalWrite(manPin, LOW);
		}
		else if (canIn[1] == 1)
		{
			digitalWrite(autoPin, HIGH);
			digitalWrite(manPin, LOW);
		}
		else if (canIn[1] == 2)
		{
			digitalWrite(autoPin, HIGH);
			digitalWrite(manPin, HIGH);
		}
	}
	else if (msgType == 4)					//Set 1-wire relays to desired state
	{
		//setRelays(canIn[1], canIn[2]);
	}
	else if (msgType == 5)					//Print Current Message
	{
		Serial.print(sendingNode());
		Serial.print(",");
		for (int i = 0; i < sizeof(canIn); i++)
		{
			Serial.print(canIn[i]);
			Serial.print(",");
		}
		Serial.println();
	}
	else if (msgType == 6)					//Set Function Type
	{
		function = canIn[1];
		functionMode = canIn[2];

		if (canIn[3] > 0) delayMultiplier = canIn[3];
		else delayMultiplier = 0;

		if (function == 6) ChangeBaudRate(canIn[2]);

		if (function == 9) nodeStatus[0] = 9;
		else nodeStatus[0] = 5;
	}
	else if (msgType == 7)
	{
		if (canIn[1] == 1)
		{
			towerSelected = true;
			selectedState = canIn[2];
			if (selectedState == 1)
			{
				wipeColor(yellow, 0, 1, firstPixel(3), lastPixel(4));
				sunState = true;
			}
			else if (selectedState == 2)
			{
				wipeColor(red, 0, 1, firstPixel(2), lastPixel(2));
				alarmState = true;
			}

		}
		else
		{
			towerSelected = false;

			if (sunState)
			{
				sunState = false;
				solidColor(blue, 0, firstPixel(3), lastPixel(4));
			}
			if (alarmState)
			{
				alarmState = false;
				solidColor(blue, 0, firstPixel(2), lastPixel(2));
			}

		}
	}
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>Change Game Modes </method>
////////////////////////////////////////////////////////////////////////////////////////////////////
//Game Mode for Canbus only
void gamePlayCanbus()
{
	if (gameMode == 1)	//Ready
	{
		if (gameModeChanged)
		{
			wipeColor(blue, 0, 1, 0, stripLength);
			wipeColor(off, 0, 1, 0, stripLength);
			gameModeChanged = false;
			complete = false;
		}
	}
	else if (gameMode == 2)	//Start
	{
		//not used at this time
	}
	else if (gameMode == 3)	//Autonomous
	{
		if (gameModeChanged)
		{
			if(!sunState) solidColor(blue, 0, 0, stripLength);
			else
			{
				solidColor(yellow, 0, firstPixel(3), lastPixel(4));
				solidColor(blue, 0, firstPixel(1), lastPixel(2));
			}

			gameModeChanged = false;

			complete = false;

			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, LOW);
			}
		}

		/*if (!complete)
		{
			if (towerSelected)
			{
				solidColor(yellow, 0, 0, stripLength);
			}
			else
			{
				solidColor(blue, 0, 0, stripLength);
			}
		}*/
	}
	else if (gameMode == 4)	//Man-Tonomous
	{
		if (gameModeChanged)
		{

			gameModeChanged = false;
			complete = false;

			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, HIGH);
			}
		}

		//if (!complete)
		//{
		//	if (towerSelected)
		//	{
		//		solidColor(yellow, 0, 0, stripLength);
		//	}
		//	else
		//	{
		//		solidColor(blue, 0, 0, stripLength);
		//	}
		//}
	}
	else if (gameMode == 5)	//Manual Mode
	{
		if (gameModeChanged)
		{
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, HIGH);
			}
			wipeColor(blue, 0, 1, 0, stripLength);
			wipeColor(off, 0, 1, 0, stripLength);
			if (!sunState) solidColor(blue, 0, 0, stripLength);
			else
			{
				solidColor(yellow, 0, firstPixel(3), lastPixel(4));
				solidColor(blue, 0, firstPixel(1), lastPixel(2));
			}
			gameModeChanged = false;
			complete = false;
		}

		//if (!complete)
		//{
		//	if (towerSelected)
		//	{
		//		solidColor(yellow, 0, 0, stripLength);
		//	}
		//	else
		//	{
		//		solidColor(off, 0, 0, stripLength);
		//	}
		//}
	}
	else if (gameMode == 6)	//End
	{
		if (gameModeChanged)
		{
			gameModeChanged = false;
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, LOW);
				digitalWrite(manPin, LOW);
			}
		}
	}
	else if (gameMode == 7)	//Field Off
	{
		if (gameModeChanged)
		{
			solidColor(off, 0, 0, stripLength);
			gameModeChanged = false;
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, LOW);
				digitalWrite(manPin, LOW);
			}
		}

	}
	else if (gameMode == 8)	//Debug Mode
	{
		if (gameModeChanged)
		{
			wipeColor(blue, 0, 1, 0, stripLength);
			wipeColor(yellow, 0, 1, 0, stripLength);
			wipeColor(off, 0, 1, 0, stripLength);
			wipeColor(blue, 0, 1, 0, stripLength);

			gameModeChanged = false;

			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, HIGH);
			}


		}

		//if (towerSelected)
		//{
		//	solidColor(yellow, 0, 0, stripLength);
		//}
		//else
		//{
		//	solidColor(blue, 0, 0, stripLength);
		//}
	}
	else  //Reset	
	{
		if (gameModeChanged)
		{
			solidColor(off, 0, 0, stripLength);
			towerSelected = false;
			updateInputs();
			gameModeChanged = false;
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, LOW);
				digitalWrite(manPin, LOW);
			}
		}
	}
}

//Game Play Test Mode - All towers randomly respond 
void gamePlayRandomTest()
{
	if (gameMode == 1)	//Ready
	{
		if (gameModeChanged)
		{
			wipeColor(yellow, 0, 1, 0, stripLength);
			wipeColor(off, 0, 1, 0, stripLength);
			gameModeChanged = false;
			complete = false;
		}
	}
	else if (gameMode == 2)	//Start
	{
		//not used at this time
	}
	else if (gameMode == 3)	//Autonomous
	{
		if (gameModeChanged)
		{
			solidColor(blue, 0, 0, stripLength);
			gameModeChanged = false;

			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, LOW);
			}
		}


		if (!complete)
		{
			//Auto Test Code
		}
	}
	else if (gameMode == 4)	//Man-Tonomous
	{
		if (gameModeChanged)
		{
			gameModeChanged = false;
			complete = false;

			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, HIGH);
			}
		}

		if (!complete)
		{
			//ManTon Test Code
		}
	}
	else if (gameMode == 5)	//Manual Mode
	{
		if (gameModeChanged)
		{
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, HIGH);
			}
			wipeColor(blue, 0, 1, 0, stripLength);
			wipeColor(off, 0, 1, 0, stripLength);
			gameModeChanged = false;
			complete = false;
		}

		if (!complete)
		{
			//Manual Test Coe
		}
	}
	else if (gameMode == 6)	//End
	{
		if (gameModeChanged)
		{
			gameModeChanged = false;
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, LOW);
				digitalWrite(manPin, LOW);
			}
		}
	}
	else if (gameMode == 7)	//Field Off
	{
		if (gameModeChanged)
		{
			solidColor(off, 0, 0, stripLength);
			gameModeChanged = false;
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, LOW);
				digitalWrite(manPin, LOW);
			}
		}

	}
	else if (gameMode == 8)	//Debug Mode
	{
		if (gameModeChanged)
		{
			wipeColor(blue, 0, 1, 0, stripLength);
			wipeColor(yellow, 0, 1, 0, stripLength);
			wipeColor(off, 0, 1, 0, stripLength);
			gameModeChanged = false;
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, HIGH);
			}
		}

		//Debug Test code
	}
	else  //Reset	
	{
		if (gameModeChanged)
		{
			solidColor(off, 0, 0, stripLength);
			towerSelected = false;
			updateInputs();
			gameModeChanged = false;
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, LOW);
				digitalWrite(manPin, LOW);
			}
		}
	}
}

//Game Play Test Mode - All Towers Respond at the same time with default color
void gamePlaySpeedTest()
{
	if (gameMode == 1)	//Ready
	{
		if (gameModeChanged)
		{
			wipeColor(red, 0, 1, 0, stripLength);
			wipeColor(off, 0, 1, 0, stripLength);
			gameModeChanged = false;
			complete = false;
		}
	}
	else if (gameMode == 2)	//Start
	{
		//not used at this time
	}
	else if (gameMode == 3)	//Autonomous
	{
		if (gameModeChanged)
		{
			if (!sunState) solidColor(blue, 0, 0, stripLength);
			else
			{
				solidColor(yellow, 0, firstPixel(3), lastPixel(4));
				solidColor(blue, 0, firstPixel(1), lastPixel(2));
			}
			gameModeChanged = false;

			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, LOW);
			}
		}
		if (!complete)
		{
			//Auto Test Coe
		}
	}
	else if (gameMode == 4)	//Man-Tonomous
	{
		if (gameModeChanged)
		{
			gameModeChanged = false;
			complete = false;

			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, HIGH);
			}
		}
		if (!complete)
		{
			//Auto Test Code
		}
	}
	else if (gameMode == 5)	//Manual Mode
	{
		if (gameModeChanged)
		{
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, HIGH);
			}
			wipeColor(blue, 0, 1, 0, stripLength);
			wipeColor(off, 0, 1, 0, stripLength);
			if (!sunState) solidColor(blue, 0, 0, stripLength);
			else
			{
				solidColor(yellow, 0, firstPixel(3), lastPixel(4));
				solidColor(blue, 0, firstPixel(1), lastPixel(2));
			}
			gameModeChanged = false;
			complete = false;
		}

		if (!complete)
		{
			//Manual Test Code
		}
	}
	else if (gameMode == 6)	//End
	{
		if (gameModeChanged)
		{
			gameModeChanged = false;
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, LOW);
				digitalWrite(manPin, LOW);
			}
		}
	}
	else if (gameMode == 7)	//Field Off
	{
		if (gameModeChanged)
		{
			solidColor(off, 0, 0, stripLength);
			gameModeChanged = false;
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, LOW);
				digitalWrite(manPin, LOW);
			}
		}

	}
	else if (gameMode == 8)	//Debug Mode
	{
		if (gameModeChanged)
		{
			wipeColor(blue, 0, 1, 0, stripLength);
			wipeColor(yellow, 0, 1, 0, stripLength);
			wipeColor(off, 0, 1, 0, stripLength);
			gameModeChanged = false;
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, HIGH);
				digitalWrite(manPin, HIGH);
			}
		}
		//Debug Code
	}
	else  //Reset	
	{
		if (gameModeChanged)
		{
			solidColor(off, 0, 0, stripLength);
			towerSelected = false;
			updateInputs();
			gameModeChanged = false;
			if ((nodeID == 3) || (nodeID == 8))
			{
				digitalWrite(autoPin, LOW);
				digitalWrite(manPin, LOW);
			}
		}
	}
}

//Starts Mantonomous Mode on default Color's Side of the field
void StartManTon()
{
	Serial.println("Monton Message Sent");
	int r = random(1, 51);
	uint32_t dA = address(defaultSide, nodeID);
	byte message[8] = { 2, 4, 0, 0, 0, 0, 0, 0 };	//Change Game Mode on all tower on this side of the field
	delay(r * delayMultiplier);
	CAN.sendMsgBuf(dA, 0, sizeof(message), message, 2);	//Send message
}

//Sends out one message when placed in Start
void NetworkResponseTest()
{
	if (gameMode == 1)	//Ready
	{
		if (gameModeChanged)
		{
			wipeColor(red, 0, 1, 0, stripLength);
			wipeColor(white, 0, 1, 0, stripLength);
			gameModeChanged = false;
			complete = false;
		}
	}
	else if (gameMode == 2)	//Start
	{
		if (gameModeChanged)
		{
			gameModeChanged = false;
			complete = false;
			messagesSent++;
			nodeStatus[7] = messagesSent;
			nodeStatus[6] = messagesRecieved;
			report(0, commandNode);
		}

	}
	else if (gameMode == 7)	//Field Off
	{
		if (gameModeChanged)
		{
			report(0, commandNode);
			solidColor(off, 0, 0, stripLength);
			gameModeChanged = false;
		}

	}
	else  //Reset	
	{
		if (gameModeChanged)
		{
			messagesSent = 0;
			messagesRecieved = 0;
			nodeStatus[7] = messagesSent;
			nodeStatus[6] = messagesRecieved;

			solidColor(off, 0, 0, stripLength);
			towerSelected = false;
			updateInputs();
			gameModeChanged = false;
		}
	}

}

//Sends out a burst of 255 messages when placed in Start
void NetworkSpeedTest()
{
	if (gameMode == 1)	//Ready
	{
		if (gameModeChanged)
		{
			wipeColor(red, 0, 1, 0, stripLength);
			wipeColor(yellow, 0, 1, 0, stripLength);
			gameModeChanged = false;
			complete = false;
		}
	}
	else if (gameMode == 2)	//Start
	{
		if (gameModeChanged)
		{
			gameModeChanged = false;
			complete = false;
		}

		if (messagesSent < 255)
		{
			messagesSent++;
			nodeStatus[7] = messagesSent;
			nodeStatus[6] = messagesRecieved;
			report(0, commandNode);
		}
	}
	else if (gameMode == 7)	//Field Off
	{
		if (gameModeChanged)
		{
			report(0, commandNode);
			solidColor(off, 0, 0, stripLength);
			gameModeChanged = false;
		}

	}
	else  //Reset	
	{
		if (gameModeChanged)
		{
			messagesSent = 0;
			messagesRecieved = 0;
			nodeStatus[7] = messagesSent;
			nodeStatus[6] = messagesRecieved;

			solidColor(off, 0, 0, stripLength);
			towerSelected = false;
			updateInputs();
			gameModeChanged = false;
		}
	}

}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>NeoPixel Methods</method>
////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma region NeoPixel Methods

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>neoPix() </method>
///
/// <summary>Used to Parse Neopixel Commands </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
void neoPix()
{
	//Set Local Variables
	uint32_t _color = colorValue(canIn[1]);
	uint8_t _firstPixel = 0;
	uint8_t _lastPixel = stripLength;
	uint8_t _mode = canIn[2];
	uint8_t _ring = canIn[4];
	uint8_t _wait = canIn[5];

	if (canIn[3] != 0)							//If Byte 3 is not 0 (all pixels)
	{
		_firstPixel = firstPixel(canIn[3]);		//Set firsPixel
		_lastPixel = lastPixel(canIn[3]);		//Set lastPixel
	}

	if (_mode == 0)															//If Mode is 0
	{
		solidColor(_color, _wait, 0, stripLength);							//Turn on Ligth Solid Color
	}
	else if (_mode == 1)													//If Mode is 1
	{
		solidColor(_color, _wait, _firstPixel, _lastPixel);					//Turn ring(s) solid color
	}
	else if (_mode == 2)													//Mode is 2
	{
		flashColorLatch(_color, _wait, _ring, _firstPixel, _lastPixel);		//Flash Color and latch to that color
	}
	else if (_mode == 3)													//If Mode is 3
	{
		flashColor(_color, _wait, _ring, _firstPixel, _lastPixel);			//Flash Color and turn off
	}
	else if (_mode == 4)													//If Mode is 4
	{
		wipeColor(_color, _wait, _ring, _firstPixel, _lastPixel);			//Wipe color up the strip
	}
	else if (_mode == 5)													//If Mode is 5
	{
		testPattern();														//Run Test Pattern
	}
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	colorValue() </method>
///
/// <summary>	Used to parse the color valuse based on current message </summary>
///
/// <param name="clrVlu">	current color value in message </param>
///
/// <returns>	returns color value for NeoPixels </returns>
////////////////////////////////////////////////////////////////////////////////////////////////////
uint32_t colorValue(uint8_t clrVlu)
{
	if (clrVlu == 1) return red;
	else if (clrVlu == 2) return green;
	else if (clrVlu == 3) return yellow;
	else if (clrVlu == 4) return blue;
	else if (clrVlu == 5) return white;
	else return off;
}
uint8_t colorCode(uint32_t _color)
{
	if (_color == red) return 1;
	else if (_color == green) return 2;
	else if (_color == yellow) return 3;
	else if (_color == blue) return 4;
	else if (_color == white) return 5;
	else return 0;
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	firstPixel() </method>
///
/// <summary>	calculates the first pixel location based on current message. </summary>
///
/// <param name="ringValue">	The ring value in current message </param>
///
/// <returns>	first pixel location </returns>
////////////////////////////////////////////////////////////////////////////////////////////////////
uint8_t firstPixel(uint8_t ringValue)
{
	uint8_t _firstPixel;

	//If ring value is greater than 0 then calculate the first pixel
	if (ringValue > 0)
	{
		_firstPixel = (pixPerRing * ringValue) - pixPerRing;
	}
	//Else it will be zero
	else
	{
		_firstPixel = 0;
	}

	//Return Value
	return _firstPixel;
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	lastPixel() </method>
///
/// <summary>	calculates the last pixel location based on current message </summary>
///
/// <param name="ringValue">	The ring value in current message </param>
///
/// <returns>	Returns last pixel location </returns>
////////////////////////////////////////////////////////////////////////////////////////////////////
uint8_t lastPixel(uint8_t ringValue)
{
	if (ringValue > 0) return 8 * ringValue;
	else return stripLength;
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	testPattern() </method>
///
/// <summary>	runs a short little light show to make sure all colors are working </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
void testPattern()
{
	nodeStatus[1] = 6;	//Color
	nodeStatus[2] = 5;	//Mode
	solidColor(white, 500, 0, stripLength);
	delay(100);
	solidColor(red, 500, 0, stripLength);
	delay(100);
	solidColor(green, 500, 0, stripLength);
	delay(100);
	solidColor(yellow, 500, 0, stripLength);
	delay(100);
	solidColor(blue, 500, 0, stripLength);
	delay(100);
	solidColor(off, 500, 0, stripLength);
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	wipeColor() </method>
///
/// <summary>	Fills in neopixels one after another till the ring or stip is filled </summary>
///
/// <param name="_color">  		color </param>
/// <param name="_wait">	   	wait between pixels </param>
/// <param name="_times">   	number of times the wipe should repeat </param>
/// <param name="_startPix">	first pixel to light </param>
/// <param name="_endPix">  	last pixel to light </param>
////////////////////////////////////////////////////////////////////////////////////////////////////
void wipeColor(uint32_t _color, uint8_t _wait, uint8_t _times, uint8_t _startPix, uint8_t _endPix)
{
	nodeStatus[1] = colorCode(_color);
	nodeStatus[2] = 4;

	//If wait is zero set to defualt
	if (_wait == 0) _wait = 10;

	//if time is zero set to 1 so it will at least do it once
	if (_times == 0) _times = 1;

	//loop for the command number of times
	for (uint8_t i = 0; i < _times; i++)
	{
		//If looping more than once turn off the strip
		if (_times > 1)
		{
			//turn off pixels one at a time starting with the first and ending with the last
			for (uint8_t i = _startPix; i < _endPix; i++)
			{
				light.setPixelColor(i, off);	//Set pixel to off
				light.show();					//Turn it on
				delay(_wait);					//Wait
			}
		}
		//turn on pixels one at a time starting with the first and ending with the last
		for (uint8_t i = _startPix; i < _endPix; i++)
		{
			light.setPixelColor(i, _color);	//Set pixel to the color
			light.show();					//Turn it on
			delay(_wait);					//Wait
		}

	}
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	solidColor </method>
///
/// <summary>	turns on all pixels in a ring or strip to the same color at the same time </summary>
///
/// <param name="_color">  		color </param>
/// <param name="_wait">	   	wait before the next command </param>
/// <param name="_startPix">	first pixel to light </param>
/// <param name="_endPix">  	last pixel to light </param>
////////////////////////////////////////////////////////////////////////////////////////////////////
void solidColor(uint32_t _color, uint8_t _wait, uint8_t _startPix, uint8_t _endPix)
{
	nodeStatus[1] = colorCode(_color);
	nodeStatus[2] = 1;

	//If wait is 0 set to default
	if (_wait == 0) _wait = 10;

	//Set pixels to all the same color starting with the first and ending with the last
	for (uint16_t i = _startPix; i < _endPix; i++)
	{
		light.setPixelColor(i, _color);
	}
	light.show();		//Let there be light
	delay(_wait);		//Wait
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	flashColor() </method>
///
/// <summary>	Flashes one color on on and off a set number of times </summary>
///
/// <param name="_color">  		color </param>
/// <param name="_wait">	   	wait between pixels </param>
/// <param name="_times">   	number of times the wipe should repeat </param>
/// <param name="_startPix">	first pixel to light </param>
/// <param name="_endPix">  	last pixel to light </param>
////////////////////////////////////////////////////////////////////////////////////////////////////
void flashColor(uint32_t _color, uint8_t _wait, uint8_t _times, uint8_t _startPix, uint8_t _endPix)
{
	nodeStatus[1] = colorCode(_color);
	nodeStatus[2] = 3;

	//If Wait is 0 set default
	if (_wait == 0) _wait = 350;

	//If times is 0 set to 1 so it will at least do it once
	if (_times == 0) _times = 1;

	//Loop the commanded number of times
	for (int i = 0; i < _times; i++)
	{
		solidColor(_color, _wait, _startPix, _endPix);	//Set all pixels on that color
		solidColor(off, _wait, _startPix, _endPix);		//Set all pixels off
	}
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	flashColorLatch() </method>
///
/// <summary>	Flashes one color on on and off a set number of times
/// 			and then latches that color </summary>
///
/// <param name="_color">  		color </param>
/// <param name="_wait">	   	wait between pixels </param>
/// <param name="_times">   	number of times the wipe should repeat </param>
/// <param name="_startPix">	first pixel to light </param>
/// <param name="_endPix">  	last pixel to light </param>
////////////////////////////////////////////////////////////////////////////////////////////////////
void flashColorLatch(uint32_t _color, uint8_t _wait, uint8_t _times, uint8_t _startPix, uint8_t _endPix)
{
	nodeStatus[1] = colorCode(_color);
	nodeStatus[2] = 2;

	//If Wait is 0 set default
	if (_wait == 0) _wait = 350;

	//If times is 0 set to 1 so it will at least do it once
	if (_times == 0) _times = 1;

	//Loop the commanded number of times
	for (int i = 0; i < _times; i++)
	{
		solidColor(_color, _wait, _startPix, _endPix);	//Set all pixels on that color
		solidColor(off, _wait, _startPix, _endPix);		//Set all pixels off
	}

	solidColor(_color, _wait, _startPix, _endPix);		//Set all pixels on that color
}
#pragma endregion

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>Status and Report Methods</method>
////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma region Status and Report Methods

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	Reports() </method>
///
/// <summary>Used to Report Status of Node </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
void report(uint8_t check, uint32_t dAddress)
{
	int r = random(1, 11);
	if (check) updateInputs();									//Update Inputs
	uint32_t dA = address(dAddress, nodeID);					//Get address
	//delay(r * delayMultiplier);									//Delay a random amount of time
	delay(delayMultiplier);
	int stat = CAN_FAIL;
	int timeOut = 0;

	do
	{
		timeOut++;
		stat = CAN.sendMsgBuf(dA, 0, sizeof(nodeStatus), nodeStatus, 2);	//Send message using only one buffer
		//Serial.print(messagesSent);
		//Serial.print(" - Stat = ");
		//Serial.println(stat);
	} while ((stat != CAN_OK) && (timeOut < 5));
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	updateInputs </method>
///
/// <summary>	Updates all inputs pins on this node </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
void updateInputs()
{
	//Loop through all pins
	for (uint8_t i = 0; i < sizeof(inputPins); i++)
	{
		//If pin is not 99 it is used
		if (inputPins[i] != 99)
		{
			checkInput(i);	//Check input
		}
	}

}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	checkInput() </method>
///
/// <summary>	Checks state of given inputs and updates node status 
/// 			assumes input goes to ground to close circuit</summary>
///
/// <param name="input">	input pin to check </param>
///
/// <returns>	true if input is low, false if input is high </returns>
////////////////////////////////////////////////////////////////////////////////////////////////////
boolean checkInput(uint8_t input)
{
	//Read pin state
	uint8_t newState = digitalRead(inputPins[input]);
	//Reverse Logic
	//if (newState == 0) newState = 1;
	//else newState = 0;

	//Get old state
	uint8_t oldState = inputStates[input];

	//If button state has changed 
	if (oldState != newState)
	{
		if (newState == 1)
		{
			inputStates[input] = 1;							//Update input status
			nodeStatus[4] |= (1 << input);	//Update node status
			return true;
		}
		else
		{
			inputStates[input] = 0;							//Update input status
			nodeStatus[4] &= ~(1 << input);	//Update node status
			return false;
		}
	}
	else
	{
		if (newState == 1) return true;
		else return false;
	}
}
#pragma endregion

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>CANBUS Methods</method>
////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma region CANBUS Methods

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	address() </method>
///
/// <summary>	calculates the address given the sending node and destination node's ID </summary>
///
/// <param name="destination">	Destination Node's ID </param>
/// <param name="id">		  	Sending Node's ID </param>
///
/// <returns>	The combined Address  </returns>
////////////////////////////////////////////////////////////////////////////////////////////////////
uint32_t address(uint32_t _destination, uint32_t _id)
{
	return ((_id << 5) | _destination);		//Sending node is the 5 last bits, destination is the first 5 bits
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>	sendingNode() </method>
///
/// <summary>	gets the sending nodes ID </summary>
///
/// <returns>	returns the sending nodes ID </returns>
////////////////////////////////////////////////////////////////////////////////////////////////////
uint32_t sendingNode()
{
	uint32_t node = CAN.getCanId();	//Get whole ID
	node = (node >> 5);
	return node;				//Last 5 bits are the sending nodes ID
}

void ChangeBaudRate(byte rate)
{
	Serial.print("CAN-BUS Startup: ");
	//If CANBUS begins 
	if (CAN_OK == CAN.begin(rate))						// init can bus : baudrate = 50k
	{
		wipeColor(green, 0, 0, firstPixel(3), lastPixel(3));	//If okay light ring green
		Serial.print(rate);
		Serial.println(" - OK");									//Report Okay

	}
	else
	{
		Serial.println("Not Good, check your pin# and connections");	//Report a problem
		wipeColor(yellow, 0, 0, firstPixel(3), lastPixel(3));			//If not okay light ring yellow												//If startup failed try again
	}
}
#pragma endregion

