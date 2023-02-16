#include "sw_led.h"
#include <hidef.h> 
#include "derivative.h"

//
void SWL_Init (void)
{
    
}

//
void SWL_ON (SWL_LEDColour led)
{
    PT1AD1 |= led;
}

//
void SWL_OFF (SWL_LEDColour led)
{
    PT1AD1 &= ~led;
}

//
void SWL_TOG (SWL_LEDColour led)
{
    PT1AD1 ^= led;
}