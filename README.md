# SU25_PRN232_SE1730_ASM4_SE182896_TienDM

## DNA Testing System - SOAP Services

Hệ thống quản lý DNA Testing với SOAP API Services và Console Client Application.

## 🚀 Cách chạy nhanh

### 1. Start SOAP API Server

```bash
cd DNATestingSystem.SoapAPIServices.TienDM
dotnet run
```

Server chạy trên: https://localhost:7077 và http://localhost:5077

### 2. Run Console Client

**Quick Test:**

```bash
cd DNATestingSystem.SoapClient.ConsoleApp.TienDM
dotnet run -- --test
```

**Interactive Mode:**

```bash
cd DNATestingSystem.SoapClient.ConsoleApp.TienDM
dotnet run
```

**Chọn Protocol khi được hỏi:**

- **Option 1**: HTTPS (Recommended) - `https://localhost:7077`
- **Option 2**: HTTP - `http://localhost:5077`

## ✅ Đã khắc phục các lỗi

### ❌ Lỗi "URI scheme 'https' is invalid"

**Nguyên nhân:** SOAP client cố gắng kết nối HTTPS với BasicHttpBinding.

**✅ Giải pháp đã áp dụng:**

- Console App tự động detect HTTP/HTTPS và sử dụng binding phù hợp
- Hỗ trợ cả BasicHttpBinding (HTTP) và BasicHttpsBinding (HTTPS)
- User có thể chọn protocol khi chạy app

### ❌ Lỗi deserialization với DateOnly/TimeOnly

**Nguyên nhân:** SOAP XML response sử dụng DateTime nullable thay vì DateOnly/TimeOnly.

**✅ Giải pháp đã áp dụng:**

- Cập nhật models để sử dụng `DateTime?` và `TimeSpan?`
- Tương thích với XML response format từ SOAP service

## 🧪 Test Results

**SOAP Connection Test:**

```
===========================================
    DNA Testing System SOAP Client
===========================================
Running simple SOAP test...
Testing connection to: https://localhost:7077/AppointmentsTienDmSoapService.asmx
✓ Connected successfully!
⏳ Calling GetAllAppointments...
✓ Retrieved appointments successfully
✓ Test completed successfully!
```

## 🛠️ Khắc phục lỗi "URI scheme 'https' is invalid"

Lỗi này xảy ra khi SOAP client cố gắng kết nối HTTPS với BasicHttpBinding.

**Đã được khắc phục:**

- ✅ Console App tự động detect HTTP/HTTPS và sử dụng binding phù hợp
- ✅ Hỗ trợ cả BasicHttpBinding (HTTP) và BasicHttpsBinding (HTTPS)
- ✅ User có thể chọn protocol khi chạy app

## 📁 Cấu trúc dự án

```
├── DNATestingSystem.SoapAPIServices.TienDM/     # SOAP API Server
├── DNATestingSystem.SoapClient.ConsoleApp.TienDM/ # Console Client
├── DNATestingSystem.Repository.TienDM/          # Repository Layer
├── DNATestingSystem.Services.TienDM/            # Service Layer
├── DNATestingConsoleApp/                        # Basic Console App
├── DNATestingSystem-SOAP-Services.postman_collection.json
├── DNATestingSystem-Environment.postman_environment.json
└── SOAP-TESTING-GUIDE.md
```

## 🧪 Testing với Postman

1. Import `DNATestingSystem-SOAP-Services.postman_collection.json`
2. Import `DNATestingSystem-Environment.postman_environment.json`
3. Sử dụng các endpoints:
   - HTTPS: `{{baseUrl}}` (https://localhost:7299)
   - HTTP: `{{baseUrlHttp}}` (http://localhost:5299)

## 📖 Tài liệu chi tiết

- [SOAP Testing Guide](SOAP-TESTING-GUIDE.md)
- [Console App README](DNATestingSystem.SoapClient.ConsoleApp.TienDM/README.md)
