using System;
using System.Collections;
using NativeWifi;
using System.Text;

namespace project
{
    class WiFiHelper
    {
        // Assume the SSID is first command line argument
        static void Main(string[] args)
        {
            WlanClient client = new WlanClient();
            if (client.Interfaces.Length > 1)
            {
                Console.WriteLine("ERROR: More than one WiFi interface. Must specify which one! Implementation TODO");
                Environment.Exit(0);
            }
            else if (client.Interfaces.Length == 0) 
            {
                Console.WriteLine("ERROR: No WiFi interfaces found.");
                Environment.Exit(0);
            }

            WlanClient.WlanInterface wlanIface = client.Interfaces[0];
            String openWifiSSID = args[0];

            ConnectToOpenWifiNetwork(wlanIface, openWifiSSID);
        
            /* 
            ListAvailableNetworks(wlanIface);
            ArrayList openSSIDs = GetAvailableOpenWifiNetworkSSIDs(wlanIface);

            // Connect to the first one
            String openSSID_str = openSSIDs[0].ToString();
            ConnectToOpenWifiNetwork(wlanIface, openSSID_str);
            */
        } 

        static string GetStringForSSID(Wlan.Dot11Ssid ssid)
        {
            return Encoding.ASCII.GetString( ssid.SSID, 0, (int) ssid.SSIDLength );
        }

        // Connects to an Open WiFi Network based on it's SSID (profileName)
        // Note that an "Open Wifi Network" is one without a CipherAlgorithm
        static void ConnectToOpenWifiNetwork(WlanClient.WlanInterface wlanIface, string profileName)
        {
            string profileXml = String.Format("<?xml version=\"1.0\"?><WLANProfile xmlns=\"http://www.microsoft.com/networking/WLAN/profile/v1\"><name>{0}</name><SSIDConfig><SSID><name>{0}</name></SSID></SSIDConfig><connectionType>ESS</connectionType><MSM><security><authEncryption><authentication>open</authentication><encryption>none</encryption><useOneX>false</useOneX></authEncryption></security></MSM></WLANProfile>", profileName); 
            wlanIface.SetProfile( Wlan.WlanProfileFlags.AllUser, profileXml, true );
            wlanIface.Connect( Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName );
            Console.WriteLine("Attempted to connect to network with SSID {0}", profileName);
        } 

        static void ListAvailableNetworks(WlanClient.WlanInterface wlanIface)
        {
            // Lists all available networks
            Wlan.WlanAvailableNetwork[] networks = GetAvailableWifiNetworks(wlanIface); // = wlanIface.GetAvailableNetworkList( 0 );
            foreach ( Wlan.WlanAvailableNetwork network in networks )
            {                     
                Console.WriteLine("Found network with SSID {0} with cipher algorithm: {1}", GetStringForSSID(network.dot11Ssid), network.dot11DefaultCipherAlgorithm);
            }
        }

        // Returns a string ArrayList of SSIDs of Open Wifi Networks
        static ArrayList GetAvailableOpenWifiNetworkSSIDs(WlanClient.WlanInterface wlanIface)
        {
            Wlan.WlanAvailableNetwork[] networks = GetAvailableWifiNetworks(wlanIface);

            ArrayList openNetworkSSIDs = new ArrayList(); // list of SSIDs as strings
            foreach (Wlan.WlanAvailableNetwork network in networks)
            {
                if (network.dot11DefaultCipherAlgorithm == Wlan.Dot11CipherAlgorithm.None) 
                {
                    string profileName = GetStringForSSID(network.dot11Ssid);
                    openNetworkSSIDs.Add(profileName);
                    Console.WriteLine("Found an open network: {0}", profileName);
                }            
            }
            return openNetworkSSIDs;
        }

        static Wlan.WlanAvailableNetwork[] GetAvailableWifiNetworks(WlanClient.WlanInterface wlanIface)
        {
            // Lists all available networks
            Wlan.WlanAvailableNetwork[] networks = wlanIface.GetAvailableNetworkList( 0 );
            return networks;
        }
    }
}
