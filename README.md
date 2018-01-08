# Evil-Twin-WiFi-APs

This repository partially duplicates an internal University of Minnesota GitHub repo containing a semester project from CSCI 5271: Computer Security. Our project focused on Evil Twin Access Points by replicating certain defenses and attacks. 

One of my primary contribution to the project focused on programmatically connecting to wireless networks on Windows and Linux machines, presented here as WiFi Helper.

## WiFi Helper
### Summary
The WiFi Helper executable utilizes the [Managed WiFi C# Library](https://managedwifi.codeplex.com/), which in turn builds off of the Windows [Native WiFi API](https://msdn.microsoft.com/en-us/library/ms705969.aspx). The executable currently accepts one command line argument: the name of an open WiFi network's SSID, which the executable will attempt to connect to. 

We define an "open WiFi network" as a WiFi network with no declared cipher algorithm.


### Compilation and Execution

**Prerequisites**: You should have [Microsoft's .NET SDK](https://www.microsoft.com/net/learn/get-started/windows) installed and have the [C# compiler (csc.exe) added to your PATH environment variable](https://stackoverflow.com/questions/3425515/compiling-c-sharp-code-from-the-command-line-gives-error).

Instructions for compiling the source code into an executable are as follows. (These commands should be run in a PowerShell environment that is able to compile C# code):


```PowerShell
csc WiFiHelper.cs WlanApi.cs Interop.cs # Creates an executable WiFiHelper.exe file

./WiFiHelper "[SSID of open WiFi network here]"
```

