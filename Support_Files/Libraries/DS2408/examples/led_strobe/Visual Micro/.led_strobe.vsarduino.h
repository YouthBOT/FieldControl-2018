/* 
	Editor: http://www.visualmicro.com
	        arduino debugger, visual micro +, free forum and wiki
	
	Hardware: Arduino Uno, Platform=avr, Package=arduino
*/

#define __AVR_ATmega328P__
#define ARDUINO 101
#define ARDUINO_MAIN
#define F_CPU 16000000L
#define __AVR__
#define __cplusplus
extern "C" void __cxa_pure_virtual() {;}

static int uart_putchar (char c, FILE *stream);
void setup_stdout();
void __cxa_pure_virtual(void);
void setup(void);
void loop(void);
void setup_devices(Devices* devices, uint8_t device_count);
void display_mode(Device device);
void display_activity(Device device);
void display_state(Device device);
void print_byte(uint8_t data);
void print_address(byte* address);
void print_devices(Devices* devices, uint8_t device_count);

#include "C:\Program Files (x86)\Arduino\hardware\arduino\variants\standard\pins_arduino.h" 
#include "C:\Program Files (x86)\Arduino\hardware\arduino\cores\arduino\arduino.h"
#include "D:\YBOT\Software\Arduino Programs\queezythegreat-arduino-ds2408-7602ab6\queezythegreat-arduino-ds2408-7602ab6\DS2408\examples\led_strobe\led_strobe.pde"
