 
# 🔋 Mobile Battery Information

A simple C# xamarin application that retrieves and displays battery status information from a mobile device. This project demonstrates how to access system-level battery data using .NET and is ideal for learning how to build utility apps for mobile platforms.

---

## 📦 Overview

This project includes:

- Battery level monitoring
- Charging status detection
- Basic UI for displaying battery info
- Built with .NET for desktop or mobile environments

---

## 🚀 Getting Started

### Prerequisites

- Visual Studio (recommended)
- .NET Framework or .NET Core SDK
- Compatible device or emulator

### How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/supunsarachitha/Mobile_Battery_Information.git
   ```

2. Open `Battery_Info.sln` in Visual Studio.

3. Build and run the project.

---

## 📁 Project Structure

```
Mobile_Battery_Information/
├── Battery_Info/             # Source code folder
│   ├── MainForm.cs           # Main UI logic
│   └── Program.cs            # Entry point
├── Battery_Info.sln          # Solution file
├── README.md                 # Project documentation
└── .gitignore                # Git settings
```

---

## 🧪 Sample Code Snippet

```csharp
PowerStatus powerStatus = SystemInformation.PowerStatus;
float batteryLifePercent = powerStatus.BatteryLifePercent * 100;
bool isCharging = powerStatus.PowerLineStatus == PowerLineStatus.Online;
```

---

## 📄 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

## 👤 Author

Created by [Supun Sarachitha](https://github.com/supunsarachitha). Feel free to fork, contribute, or reach out!

---

## 🙌 Contributions

Pull requests are welcome! If you’d like to add features like battery temperature, health status, or platform-specific enhancements, go ahead and submit a PR.

---

## 📬 Contact

For questions or suggestions, open an issue or connect via GitHub.
 
