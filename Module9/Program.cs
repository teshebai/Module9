using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module9
{
    public abstract class Storage
    {
        public string Name { get; set; }
        public string Model { get; set; }

        public abstract double GetMemoryCapacity();
        public abstract double CopyData(double dataAmount);
        public abstract double GetFreeMemory();
        public abstract void DisplayInfo();
    }

    public class Flash : Storage
    {
        public double UsbSpeed { get; set; }
        public double MemoryCapacity { get; set; }

        public override double GetMemoryCapacity() => MemoryCapacity;

        public override double CopyData(double dataAmount)
        {
            return dataAmount / UsbSpeed;
        }

        public override double GetFreeMemory() => MemoryCapacity;

        public override void DisplayInfo()
        {
            Console.WriteLine($"Flash - Name: {Name}, Model: {Model}, USB Speed: {UsbSpeed}, Capacity: {MemoryCapacity} GB");
        }
    }

    public class DVD : Storage
    {
        public double ReadWriteSpeed { get; set; }
        public double Capacity { get; set; } // Single-sided or double-sided

        public override double GetMemoryCapacity() => Capacity;

        public override double CopyData(double dataAmount)
        {
            return dataAmount / ReadWriteSpeed;
        }

        public override double GetFreeMemory() => Capacity;

        public override void DisplayInfo()
        {
            Console.WriteLine($"DVD - Name: {Name}, Model: {Model}, Speed: {ReadWriteSpeed}x, Capacity: {Capacity} GB");
        }
    }

    public class HDD : Storage
    {
        public double UsbSpeed { get; set; }
        public int PartitionCount { get; set; }
        public double PartitionSize { get; set; }

        public override double GetMemoryCapacity() => PartitionCount * PartitionSize;

        public override double CopyData(double dataAmount)
        {
            return dataAmount / UsbSpeed;
        }

        public override double GetFreeMemory() => PartitionCount * PartitionSize;

        public override void DisplayInfo()
        {
            Console.WriteLine($"HDD - Name: {Name}, Model: {Model}, USB Speed: {UsbSpeed}, Partitions: {PartitionCount}, Each Size: {PartitionSize} GB");
        }
    }

    public class BackupApplication
    {
        private List<Storage> storages;

        public BackupApplication()
        {
            storages = new List<Storage>();
        }

        public void AddStorageDevice(Storage storage)
        {
            storages.Add(storage);
        }

        public double CalculateTotalMemoryCapacity()
        {
            return storages.Sum(storage => storage.GetMemoryCapacity());
        }

        public double CalculateTotalTimeForCopying(double dataAmount)
        {
            return storages.Sum(storage => storage.CopyData(dataAmount));
        }

        public int CalculateRequiredStorageDevices(double totalData, double fileSize)
        {
            int totalRequired = 0;
            foreach (var storage in storages)
            {
                int requiredForStorage = (int)Math.Ceiling(totalData / storage.GetMemoryCapacity());
                totalRequired += requiredForStorage;
            }
            return totalRequired;
        }

        public void DisplayAllStorageDevicesInfo()
        {
            foreach (var storage in storages)
            {
                storage.DisplayInfo();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var backupApp = new BackupApplication();
            backupApp.AddStorageDevice(new Flash { Name = "Kingston", Model = "DataTraveler", UsbSpeed = 3.0, MemoryCapacity = 128 });
            backupApp.AddStorageDevice(new DVD { Name = "Sony", Model = "DVR", ReadWriteSpeed = 1.2, Capacity = 4.7 });
            backupApp.AddStorageDevice(new HDD { Name = "WD", Model = "My Passport", UsbSpeed = 2.0, PartitionCount = 2, PartitionSize = 500 });

            double totalData = 565; // GB
            double fileSize = 0.78; // GB (780 MB)
            int requiredDevices = backupApp.CalculateRequiredStorageDevices(totalData, fileSize);

            Console.WriteLine($"Total Memory Capacity: {backupApp.CalculateTotalMemoryCapacity()} GB");
            Console.WriteLine($"Total Time for Copying: {backupApp.CalculateTotalTimeForCopying(fileSize)} hours");
            Console.WriteLine($"Required Number of Storage Devices: {requiredDevices}");

            backupApp.DisplayAllStorageDevicesInfo();

            Console.ReadKey();
        }
    }


}