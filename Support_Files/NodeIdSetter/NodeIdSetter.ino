#include <EEPROM.h>
#include <Adafruit_NeoPixel.h>

//CANbus NodeID
int nodeID = 9;

//EEPROM address where nodeID will be stored
int addr = 0;

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

//Function Definitions
void solidColor(uint32_t _color, uint8_t _wait, uint8_t _startPix, uint8_t _endPix);

void setup()
{
    light.begin();
    light.show();

    EEPROM.write(addr, nodeID);
    
    if (nodeID != EEPROM.read(addr))
    {
        solidColor(red, 0, 0, stripLength); 
    }
    else
    {
        solidColor(green, 0, 0, stripLength); 
    }
}

void loop()
{
    //do nothing
}

void solidColor(uint32_t _color, uint8_t _wait, uint8_t _startPix, uint8_t _endPix)
{
    //If wait is 0 set to default
    if (_wait == 0)
    {
        _wait = 10;
    }

    //Set pixels to all the same color starting with the first and ending with the last
    for (uint16_t i = _startPix; i < _endPix; i++)
    {
        light.setPixelColor(i, _color);
    }
    light.show();        //Let there be light
    delay(_wait);        //Wait
}
