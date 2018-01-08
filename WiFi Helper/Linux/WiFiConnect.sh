#!/bin/bash

BSSID=$1

wget --spider http://google.com &> /dev/null
if [ "$?" != 0 ]
then
  echo "Not connected to a Wifi Network!"
  nmcli d wifi connect "$BSSID"
else
  echo "First we will disconnect from the network!"
  SSID=$(iwgetid -r)
  nmcli con down "$SSID"
  nmcli d wifi connect "$BSSID"
fi
 
