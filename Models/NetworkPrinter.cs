
/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

using System;
using LabW05a_DeviceManagement.Enums;
using LabW05a_DeviceManagement.Interfaces;

namespace LabW05a_DeviceManagement.Models
{
    public class NetworkPrinter : Printer, INetworkable
    {
        public string? IpAddress { get; private set; }
        public bool IsConnected { get; private set; }

        public NetworkPrinter(string deviceId, string deviceName, int yearOfUse, decimal purchasePrice,
                      string printTechnology, int printedPagesCount, bool isColorPrinter = false, DeviceStatus status = DeviceStatus.Active)
               : base(deviceId, deviceName, yearOfUse, purchasePrice, printTechnology, printedPagesCount, isColorPrinter, status)
        {
            IsConnected = false;
            IpAddress = null;
        }

        public void Connect(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                throw new ArgumentException("Địa chỉ IP không được rỗng.", nameof(ipAddress));

            if (IsConnected)
                throw new InvalidOperationException($"Máy in {DeviceId} đang kết nối IP {IpAddress}. Không thể kết nối lại.");

            IpAddress = ipAddress;
            IsConnected = true;
        }

        public void Disconnect()
        {
            IsConnected = false;
            IpAddress = null;
        }

        public override string ToString()
        {
            return base.ToString() + $" | Hỗ trợ mạng: Có, IP: {IpAddress ?? "N/A"}";
        }
    }
}