/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

using LabW05a_DeviceManagement.Enums;

namespace LabW05a_DeviceManagement.Models
{
    public class Printer : Device
    {
        public string PrintTechnology { get; set; } 
        public int PrintedPagesCount { get; set; }
        public bool IsColorPrinter { get; set; } //in màu

        public Printer(string deviceId, string deviceName, int yearOfUse, decimal purchasePrice,
                       string printTechnology, int printedPagesCount, bool isColorPrinter = false, DeviceStatus status = DeviceStatus.Active)
            : base(deviceId, deviceName, yearOfUse, purchasePrice, status)
        {
            PrintTechnology = printTechnology;
            PrintedPagesCount = printedPagesCount;
            IsColorPrinter = isColorPrinter;
        }

        public override decimal CalculateAnnualMaintenanceCost()
        {
            decimal cost = PurchasePrice * 0.04m;

            if (PrintedPagesCount > 100_000)
                cost += 500_000m;

            if (IsColorPrinter)
                cost += 300_000m;

            return cost;
        }

        public override string ToString()
        {
            return base.ToString() + $" | Loại: {PrintTechnology}, Trang in: {PrintedPagesCount:N0}, Màu: {(IsColorPrinter ? "Có" : "Không")}";
        }
    }
}