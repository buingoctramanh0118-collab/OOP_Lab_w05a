
/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/
using System;
using LabW05a_DeviceManagement.Enums;
using LabW05a_DeviceManagement.Interfaces;

namespace LabW05a_DeviceManagement.Models
{
    public class Computer : Device, INetworkable
    {
        public int RamGB { get; set; }
        public string CpuType { get; set; }
        public bool HasDedicatedGpu { get; set; }

        public string? IpAddress { get; private set; }
        public bool IsConnected { get; private set; }

        public Computer(string deviceId, string deviceName, int yearOfUse, decimal purchasePrice,
                        int ramGb, string cpuType, bool hasDedicatedGpu, DeviceStatus status = DeviceStatus.Active)
            : base(deviceId, deviceName, yearOfUse, purchasePrice, status)
        {
            RamGB = ramGb;
            CpuType = cpuType;
            HasDedicatedGpu = hasDedicatedGpu;
            IsConnected = false;
            IpAddress = null;
        }

        public override decimal CalculateAnnualMaintenanceCost()
        {

            decimal cost = PurchasePrice * 0.05m;

            if (HasDedicatedGpu)
                cost += PurchasePrice * 0.02m;

            if (GetYearsInUse() > 5)
                cost += PurchasePrice * 0.01m;

            return cost;
        }

        public void Connect(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                throw new ArgumentException("Địa chỉ IP không được rỗng.", nameof(ipAddress));

            if (IsConnected)
                throw new InvalidOperationException($"Thiết bị {DeviceId} đang kết nối tại IP {IpAddress}. Không thể kết nối lại.");

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
            return base.ToString() + $" | CPU: {CpuType}, RAM: {RamGB}GB, GPU: {(HasDedicatedGpu ? "Có" : "Không")}, IP: {IpAddress ?? "N/A"}";
        }
    }
}