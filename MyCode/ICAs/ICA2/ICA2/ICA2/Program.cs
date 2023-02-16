/////////////////////////////////////////////////////////////////////////////
// 9S12X Program: Code Migration - alternates blinking a red light and a 
//                green light on the board
// Processor:     MC9S12XDP512
// Bus Speed:     20 MHz (Requires Active PLL)
// Author:        Ryley Ellis-Dye
// Details:       Program uses one increasing variable to call RED and GREEN 
//                methods, under a certain value it will produce red, over the
//                value it will produce green.
// Date:          Jan 20/2023
// Revision History : Probably would have been a good idea to track this.


/////////////////////////////////////////////////////////////////////////////
#include <hidef.h>      /* common defines and macros */
#include "derivative.h" /* derivative-specific definitions */

// other system includes or your includes go here
//#include "pll.h"
//#include <stdlib.h>
//#include <stdio.h>

/////////////////////////////////////////////////////////////////////////////
// Local Prototypes
/////////////////////////////////////////////////////////////////////////////

        static void RED(int bOn);   //Initial reference to RED for error prevention
        static void GREEN(int bOn); //Initial reference to GREEN for error prevention

/////////////////////////////////////////////////////////////////////////////
// Global Variables
/////////////////////////////////////////////////////////////////////////////
         unsigned int uiMainLoopCount = 0; //Initial declaration of value that will be cycled
         
/////////////////////////////////////////////////////////////////////////////
// Constants
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// Main Entry
/////////////////////////////////////////////////////////////////////////////
void main(void)
{
  // main entry point - these two lines must appear first
  _DISABLE_COP();
  //EnableInterrupts;
  
  /////////////////////////////////////////////////////////////////////////////
  // one-time initializations
  /////////////////////////////////////////////////////////////////////////////
            PT1AD1 &= 0x1F;    //Turns off LEDs
            DDR1AD1 = 0xE0;    //Makes LEDs outputs, switches inputs           
            ATD1DIEN1 |= 0x1F; //digital inputs on for switches (?)
  /////////////////////////////////////////////////////////////////////////////
  // main program loop
  /////////////////////////////////////////////////////////////////////////////
  for (;;)
  {


            
                ++uiMainLoopCount; //increases count value until max
                
                //Call to turn on RED
                RED(uiMainLoopCount < 0x1000);

                //Call to turn on GREEN
                GREEN(uiMainLoopCount >= 0x1000);
            
  }                   
}

/////////////////////////////////////////////////////////////////////////////
// Functions
/////////////////////////////////////////////////////////////////////////////
         static void RED (int bOn)
        {
            //Turns on red LED when true (or rather, not 0)
            if (bOn != 0)
                PT1AD1 |= (char)(((unsigned long)1 << (7)));
            else
                PT1AD1 &= 0x7F;
        }

            
        static void GREEN(int bOn)
        {
            char garbage = bOn; //superfluous, but left as close as possible

            //Turns on green LED when garbage is true, which is equivalent to when the value is not 0
            if (garbage) 
              PT1AD1|=0x20;
            
            else
            PT1AD1&=0xDF;            
        }
/////////////////////////////////////////////////////////////////////////////
// Interrupt Service Routines
/////////////////////////////////////////////////////////////////////////////
