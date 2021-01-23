using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

if (args.Length == 0)
{
    Console.WriteLine("Usage: wol <mac_address>");
}
else
{
    // original code from https://stackoverflow.com/a/65529803
    
    var target = PhysicalAddress.Parse(args[0].Replace(".", "-").Replace(":", "-"));
    var header = Enumerable.Repeat((byte)0xff, 6);
    var data = Enumerable.Repeat(target.GetAddressBytes(), 16).SelectMany(mac => mac);
    var magicPacket = header.Concat(data).ToArray();
    using var client = new UdpClient();
    client.Send(magicPacket, magicPacket.Length, new IPEndPoint(IPAddress.Broadcast, 9));
    Console.WriteLine($"Sent out magic packet to [{args[0]}]");
}
