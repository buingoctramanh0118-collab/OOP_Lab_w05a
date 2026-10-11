/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

using LabW05a_DeviceManagement.Enums;
namespace LabW05a_DeviceManagement.Models
{
    public class Projector : Device
    {
        public int Lumens { get; set; }
        public int LampHoursUsed { get; set; }

        public Projector(string deviceId, string deviceName, int yearOfUse, decimal purchasePrice,
                         int lumens, int lampHoursUsed, DeviceStatus status = DeviceStatus.Active)
            : base(deviceId, deviceName, yearOfUse, purchasePrice, status)
        {
            Lumens = lumens;
            LampHoursUsed = lampHoursUsed;
        }

        public override decimal CalculateAnnualMaintenanceCost()
        {

            decimal cost = PurchasePrice * 0.03m;

            if (LampHoursUsed > 3000)
                cost += 1_500_000m;

            return cost;
        }

        public override string ToString()
        {
            return base.ToString() + $" | Độ sáng: {Lumens} lumens, Giờ đèn: {LampHoursUsed}h";
        }
    }
}