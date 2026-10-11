
/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

using System;
using System.Collections.Generic;
using System.Text;
using LabW05a_DeviceManagement.Enums;
using LabW05a_DeviceManagement.Interfaces;
using LabW05a_DeviceManagement.Models;

namespace LabW05a_DeviceManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Kiểm thử");

            // 2 máy tính (1 máy GPU rời, 1 máy mua cách đây 6 năm)
            var pc1 = new Computer("PC001", "Dell OptiPlex 7090", 2021, 18_000_000m, 16, "Core i7", hasDedicatedGpu: false);
            var pc2 = new Computer("PC002", "Dell Precision 3650", 2019, 32_000_000m, 32, "Core i9", hasDedicatedGpu: true); // > 5 năm, có GPU

            // 2 máy in (1 máy in thường in > 100k trang, 1 máy in mạng)
            var pr1 = new Printer("PR001", "Canon LBP 2900", 2020, 4_500_000m, "Laser", printedPagesCount: 120_000, isColorPrinter: false);
            var pr2 = new NetworkPrinter("PR002", "HP LaserJet Pro", 2023, 12_000_000m, "Laser", printedPagesCount: 30_000, isColorPrinter: true);

            // 1 máy chiếu có bóng đèn > 3.000 giờ
            var pj1 = new Projector("PJ001", "Panasonic PT-LB386", 2022, 15_000_000m, lumens: 3800, lampHoursUsed: 3500, DeviceStatus.UnderMaintenance);

            // tạo 2 phòng thực hành
            var lab501 = new LabRoom("LAB501", "Phòng Lab Trí Tuệ Nhân Tạo", 40);
            var lab502 = new LabRoom("LAB502", "Phòng Lab Mạng Máy Tính", 35);

            Console.WriteLine("\n 1. THÊM THIẾT BỊ");
            lab501.AddDevice(pc1);
            lab501.AddDevice(pc2);
            lab501.AddDevice(pj1);

            lab502.AddDevice(pr1);
            lab502.AddDevice(pr2);
            Console.WriteLine(">> Đã thêm các thiết bị vào phòng LAB501 và LAB502 thành công.");

            Console.WriteLine("\n 2. THỬ THÊM THIẾT BỊ TRÙNG MÃ");
            try
            {
                var duplicatePc = new Computer("PC001", "HP ProDesk", 2022, 14_000_000m, 8, "Core i5", false);
                lab501.AddDevice(duplicatePc);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Bắt ngoại lệ thành công]: {ex.Message}");
            }

            Console.WriteLine("\n 3. IN DANH SÁCH THIẾT BỊ TỪNG PHÒNG");
            PrintRoomInfo(lab501);
            PrintRoomInfo(lab502);

            Console.WriteLine("\n 4. TỔNG CHI PHÍ BẢO TRÌ DỰ KIẾN");
            Console.WriteLine($"Tổng chi phí bảo trì {lab501.RoomName}: {lab501.CalculateAnnualMaintenanceCost():N0} VNĐ");
            Console.WriteLine($"Tổng chi phí bảo trì {lab502.RoomName}: {lab502.CalculateAnnualMaintenanceCost():N0} VNĐ");

            Console.WriteLine("\n 5. LIỆT KÊ THIẾT BỊ CẦN BẢO TRÌ");
            PrintRequiringMaintenance(lab501);
            PrintRequiringMaintenance(lab502);

            Console.WriteLine("\n 6 + 7. DUYỆT VÀ KẾT NỐI MẠNG QUA INetworkable");
            List<INetworkable> networkDevices = new List<INetworkable> { pc1, pc2, (INetworkable)pr2 };

            int index = 1;
            foreach (var netDev in networkDevices)
            {
                string ip = $"192.168.1.10{index++}";
                netDev.Connect(ip);
                Console.WriteLine($"Đã kết nối IP [{netDev.IpAddress}] cho thiết bị (IsConnected = {netDev.IsConnected})");
            }

            // Thử ngắt kết nối 1 thiết bị
            pc1.Disconnect();
            Console.WriteLine($"Sau khi ngắt kết nối PC001: IsConnected = {pc1.IsConnected}, IP = {pc1.IpAddress ?? "null"}");

            Console.WriteLine("\n Hoàn thành");
        }

        static void PrintRoomInfo(LabRoom room)
        {
            Console.WriteLine($"\n[Phòng {room.RoomId} - {room.RoomName} (Sức chứa: {room.Capacity})]");
            foreach (var d in room.Devices)
            {
                Console.WriteLine($" + {d}");
            }
        }

        static void PrintRequiringMaintenance(LabRoom room)
        {
            Console.WriteLine($"\nThiết bị cần bảo trì tại {room.RoomName}:");
            var list = room.GetDevicesRequiringMaintenance();
            if (list.Count == 0)
            {
                Console.WriteLine(" (Không có thiết bị nào)");
            }
            else
            {
                foreach (var d in list)
                {
                    Console.WriteLine($" * [{d.DeviceId}] {d.DeviceName} (Trạng thái: {d.Status}, Năm SD: {d.YearOfUse})");
                }
            }
        }
    }
}
