////////////////////////////////////////////////////////////////////////////////////////////////////
/// YBOT Comamnd Node Program
/// Used to control the command node and send data to the Tower Nodes
////////////////////////////////////////////////////////////////////////////////////////////////////

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
uint8_t brt = 25;

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
uint8_t canPin = 9;
//Can-bus Interrupt Pin (Uno Shield 0 ; Leonardo 4)
uint8_t canIntrPin = 0;
//Tower Node Number
uint32_t nodeID = 31;//(uint32_t)EEPROM.read(0);

//uint32_t nodeID = 31;
//Command Node Number
uint32_t commandNode = 31;

// Set CanBus pin
//MCP_CAN CAN(canPin);
//Can-bus message
byte canOut[8] = { 0, 0, 0, 0, 0, 0, 0, 0 };
//Interrupt flag
uint8_t canRecv = 0;
//Length of can-bus message
uint8_t canLength = 0;
//Read Buffer
byte canIn[8] = { 0, 0, 0, 0, 0, 0, 0, 0 };
//Destination Node
uint32_t destNode = 0;
//Tower Node status 
byte nodeStatus[8] = { 5, 0, 0, 0, 0, 0, 0, 0 };

#pragma endregion

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary> IO Variables </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma region IO Variables
//Input Pin number - Pin# of Filtered Digital Inputs - 99 if not used
uint8_t inputPins[6] = { 99, 99, 99, 99, 99, 99 };
//LED Pin number - 13 Uno - 23 Leonardo
uint8_t ledPin = 13;
//Button State of the Filtered Digital Inputs - 0 = open; 1 = closed
byte inputStates[6] = { 0, 0, 0, 0, 0, 0 };
uint8_t bellPin = 4;
uint8_t buzzerPin = 5;

#pragma endregion

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary> Serial Input Variables </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
byte index = 0;		//Counter for serial data
char inData[32];	//Incoming serial data
boolean newserial = false;
int xbRX = 3;		//xb RX pin
int xbTX = 2;		//xb TX pin
//SoftwareSerial xbSerial(xbRX, xbTX);	//Start serial for XBee

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary> Program Flags Variables </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
boolean towerSelected = false;		//If tower is a selected tower
boolean gameModeChanged = false;	//Game Mode Changed Flag
boolean complete = false;			//If Mode/Section has completed its task
uint8_t function = 0;				//Function Type
uint8_t functionMode = 0;			//Fuction Mode
uint8_t gameMode = 0;				//Game Mode Value - starts in Debug mode
int sender = 0;						//What sent the message: 1 = PC; 2 = CANBUS; 3 = XBee
int pc = 1;
int canbus = 2;
int xbee = 3;
int messagesSent = 0;
int messagesRecieved = 0;
int fromPC = 0;
int toPC = 0;
int alarmTower = 0;
int towerCounter = 0;

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method> Ardiuno Setup Method</method>
////////////////////////////////////////////////////////////////////////////////////////////////////
void setup()
{
	Serial.begin(115200);	//Start serial communication
	//xbSerial.begin(9600);	//Start xb serial communication

	Serial.print("Node ID: ");
	Serial.println(nodeID);

	//Setup up and turn on the onboard LED to indicated power
	pinMode(ledPin, OUTPUT);
	digitalWrite(ledPin, HIGH);
	//Setup Bell and Buzzer Pins
	pinMode(bellPin, OUTPUT);
	digitalWrite(bellPin, LOW);
	pinMode(buzzerPin, OUTPUT);
	digitalWrite(buzzerPin, LOW);

	//Setup NeoPixels and test them
	Serial.println("Neopixel Test");
	light.begin();						//Start NeoPixel light
	light.show();						// Initialize all pixels to 'off'
	testPattern();						//Test Neopixels
	solidColor(red, 0, 0, stripLength); //Set all red to begin self check

	//Setup Inputs and test to see if they are in desired state
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

	//Check to see if we can talk to the command node	
	Serial.println("CAN-BUS Communication: Command Node");

	Serial.println("-----XBee-----");
	//xbSerial.println("$,21,7,1,3,");
	//Serial.println("$,21,7,1,3,");


	delay(2000); //Visual Delay

	solidColor(off, 0, 0, stripLength);	//Turn off light

	Serial.println("Ready");


}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method> Main Loop </method>
////////////////////////////////////////////////////////////////////////////////////////////////////
void loop()
{
	if (newserial)
	{
		newserial = false;

		//If message is for the command node process the data
		if (destNode == nodeID)
		{
			for (uint8_t i = 0; i < sizeof(canIn); i++)
			{
				canIn[i] = canOut[i];	//Move message to canIn
			}
			execute();					//Execute Command
		}
		else
		{
			if (destNode == 0)
			{
				for (int i = 1; i < 10; i++)
				{
					if (canOut[7] == 1) alarmTower = i;

					if (canOut[0] == 0) report(i);
					else if (canOut[0] == 2)
					{
						gameMode = canOut[1];
						gameModeChanged = true;
						nodeStatus[3] = gameMode;
					}
				}
			}
			else
			{
				if ((canOut[2] == 2) && (canOut[0] == 7))
				{
					alarmTower = destNode;
				}

				if (canOut[0] == 0) report(destNode);
				else if (canOut[0] == 2)
				{
					gameMode = canOut[1];
					gameModeChanged = true;
					nodeStatus[3] = gameMode;
				}
			}
		}
	}

	while (Serial.available())
	{
		sender = pc;
		char aChar = Serial.read();		//Read data

		if (aChar == '$')
		{
			inData[0] = aChar;
			index = 1;
		}
		else								//If no new line keep collecting the serial data
		{
			if (inData[0] = '$')
			{
				if (aChar == '\n')				//If there is a new line
				{
					fromPC++;
					parseData();				//Parse the data that was received
					newserial = true;
				}
				else
				{
					inData[index] = aChar;			//Add next character received to the buffer
					index++;						//Increment index
					inData[index] = '\0';			//Keep NULL Terminated as last the last character
				}
			}
		}
	}

	if (function == 9)
	{
		if (functionMode == 5) NetworkResponseTest();
		else if (functionMode == 6) NetworkSpeedTest();
	}
	else gamePlayCanbus();
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
		report(31);		//Send Report to the sending Node
	}
	else if (msgType == 1)					//Set neopixels to desired state
	{
		neoPix();
	}
	else if (msgType == 2)					//Set current game mode
	{
		gameMode = canIn[1];				//Set Game Mode to the new Game Mode
		gameModeChanged = true;
		complete = false;
		nodeStatus[3] = gameMode;
	}
	else if (msgType == 3)					//Set transmitters to desired state
	{
		//Not Used
	}
	else if (msgType == 4)					//Set 1-wire relays to desired state
	{
		//setRelays(canIn[1], canIn[2]);
	}
	else if (msgType == 5)					//Print Current Message
	{
		toPC++;
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

		if (function == 6) ChangeBaudRate(canIn[2]);

		if (function == 9) nodeStatus[0] = 9;
		else nodeStatus[0] = 5;
	}
	else if (msgType == 9)
	{
		if (canIn[7] <= 255)
		{
			toPC++;
			Serial.print(sendingNode());
			Serial.print(",");
			Serial.print("9");
			Serial.print(",");
			Serial.print(fromPC);
			Serial.print(",");
			Serial.print(toPC);
			Serial.print(",");
			Serial.print(messagesRecieved);
			Serial.print(",");
			Serial.print(messagesSent);
			Serial.print(",");
			Serial.print(canIn[6]);
			Serial.print(",");
			Serial.print(canIn[7]);
			Serial.print(",");
			Serial.println();
		}
	}
	else if (msgType == 100)					//Set 1-wire relays to desired state
	{
		digitalWrite(bellPin, HIGH);
		delay(100);
		digitalWrite(bellPin, LOW);
		delay(100);
		digitalWrite(bellPin, HIGH);
		delay(100);
		digitalWrite(bellPin, LOW);
		delay(100);
	}
	else if (msgType == 101)					//Set 1-wire relays to desired state
	{
		digitalWrite(buzzerPin, HIGH);
		delay(1000);
		digitalWrite(buzzerPin, LOW);
		delay(100);
	}

}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>parseData() </method>
///
/// <summary>Used to Parse Incoming Serial Data </summary>
////////////////////////////////////////////////////////////////////////////////////////////////////
void parseData()
{
	char *token = NULL;		//Set pointer

	int counter = 0;


	//If not NULL
	if (inData[0] == '$')
	{ 
		//Break data at commas
		token = strtok(inData, ",");
		token = strtok(NULL, ",");	//Break at the next comma

		destNode = atoi(token);		//Convert message to number: First message is Destination Node
		token = strtok(NULL, ",");	//Break at the next comma
									//Iterate through the message storing each part of the message in the canOut buffer
		for (uint8_t i = 0; i < sizeof(canOut); i++)
		{
			//If token is not NULL
			if (token != NULL)
			{
				canOut[i] = atoi(token);		//Store this part in the current canOut byte
				token = strtok(NULL, ",");		//Break at the next comma
			}
			else
			{
				canOut[i] = 0;					//If the message is Null set to zero
			}
		}

		//Use for debugging
		//Serial.print("Destination Node#");
		//Serial.print(destNode);
		//Serial.print(" : Message = ");
		//for (int i = 0; i < 8; i++)
		//{
		//	Serial.print(canOut[i]);
		//	Serial.print("|");
		//}
		//Serial.println();
	}

	index = 0;

	for (uint8_t i = 0; i < sizeof(inData); i++)
	{
		inData[i] = '\0';
	}



}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <method>Change Game Modes </method>
////////////////////////////////////////////////////////////////////////////////////////////////////
void gamePlayCanbus()
{
	if (gameMode == 1)	//Standby
	{

	}
	else if (gameMode == 2)	//Start
	{

	}
	else if (gameMode == 3)	//Autonomous
	{
		if (gameModeChanged)
		{
			complete = false;
			gameMode = false;
		}
		if (!complete)
		{
			nodeStatus[6] = 0;
			nodeStatus[7] = 8;
			report(2);
			report(4);
			report(7);
			report(9);
			delay(250);

			nodeStatus[7] = 2;

			delay(250);
			report(2);
			report(4);
			report(7);
			report(9);
			nodeStatus[7] = 0;
			nodeStatus[6] = 3;
			report(21);

			complete = true;
		}

	}
	else if (gameMode == 4)	//Man-Tonomous
	{

	}
	else if (gameMode == 5)	//Manual Mode
	{
		if (gameModeChanged)
		{
			complete = false;
			alarmTower = 0;
			towerCounter = 0;
			gameModeChanged = false;
		}
		if (!complete)
		{
			//nodeStatus[6] = 0;
			//nodeStatus[7] = 8;
			//report(2);
			//report(4);
			//report(7);
			//report(9);
			//delay(1000);

			//Serial.print("Alarm Tower MM:");
			//Serial.println(alarmTower);

			nodeStatus[7] = 2;
			if (alarmTower == 2)
			{
				report(2);
				towerCounter++;
				alarmTower = 0;
			}
			else if (alarmTower == 4)
			{
				report(4);
				towerCounter++;
				alarmTower = 0;
			}
			else if (alarmTower == 7)
			{
				report(7);
				towerCounter++;
				alarmTower = 0;
			}
			else if (alarmTower == 9) 
			{
				report(9);
				towerCounter++;
				alarmTower = 0;
			}
			delay(250);

			if (towerCounter == 4)
			{
				nodeStatus[7] = 0;
				nodeStatus[6] = 3;
				report(21);
				complete = true;
			}

		}

	}
	else if (gameMode == 6)	//End
	{

	}
	else if (gameMode == 7)	//Field Off
	{

	}
	else if (gameMode == 8)	//Debug Mode
	{
		if (gameModeChanged)
		{
			wipeColor(blue, 0, 1, 0, stripLength);
			wipeColor(yellow, 0, 1, 0, stripLength);
			wipeColor(off, 0, 1, 0, stripLength);
			gameModeChanged = false;

		}
	}
	else  //Reset	
	{
		if (gameModeChanged)
		{
			messagesRecieved = 0;
			messagesSent = 0;
			toPC = 0;
			fromPC = 0;
			solidColor(off, 0, 0, stripLength);
			towerSelected = false;
			updateInputs();
			gameModeChanged = false;
		}
	}

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
			report(commandNode);
		}

	}
	else if (gameMode == 7)	//Field Off
	{
		if (gameModeChanged)
		{
			report(commandNode);
			solidColor(off, 0, 0, stripLength);
			gameModeChanged = false;
		}

	}
	else  //Reset	
	{
		if (gameModeChanged)
		{
			messagesRecieved = 0;
			messagesSent = 0;
			toPC = 0;
			fromPC = 0;
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
			report(commandNode);
		}
	}
	else if (gameMode == 7)	//Field Off
	{
		if (gameModeChanged)
		{
			report(commandNode);
			solidColor(off, 0, 0, stripLength);
			gameModeChanged = false;
		}

	}
	else  //Reset	
	{
		if (gameModeChanged)
		{
			messagesRecieved = 0;
			messagesSent = 0;
			toPC = 0;
			fromPC = 0;
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

	nodeStatus[1] = _color;													//Update color status
	nodeStatus[2] = _mode;													//Update mode status
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
	if (_wait == 0) _wait = 20;

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
void report(uint32_t _address)
{
		Serial.print(_address);
		Serial.print(",");
		for (int i = 0; i < sizeof(nodeStatus); i++)
		{
			Serial.print(nodeStatus[i]);
			Serial.print(",");
		}
		Serial.println();
		delay(10);
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
	//if (sender == canbus)
	//{
	//	uint32_t node = CAN.getCanId();	//Get whole ID
	//	return node >> 5;				//Last 5 bits are the sending nodes ID
	//}

	//else if (sender == xbee)	return 21;
	//
	//else if (destNode == nodeID) return nodeID;

	//else return destNode;
}

void ChangeBaudRate(byte rate)
{
	//Serial.print("CAN-BUS Startup: ");
	////If CANBUS begins 
	//if (CAN_OK == CAN.begin(rate))						// init can bus : baudrate = 50k
	//{
	//	wipeColor(green, 0, 0, firstPixel(3), lastPixel(3));	//If okay light ring green
	//	Serial.print(rate);
	//	Serial.println(" - OK");									//Report Okay

	//}
	//else
	//{
	//	Serial.println("Not Good, check your pin# and connections");	//Report a problem
	//	wipeColor(yellow, 0, 0, firstPixel(3), lastPixel(3));			//If not okay light ring yellow												//If startup failed try again
	//}
}
#pragma endregion


/*********************************************************************************************************
END FILE
*********************************************************************************************************/
