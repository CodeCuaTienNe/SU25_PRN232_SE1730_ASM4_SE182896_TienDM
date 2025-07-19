# DNA Testing System - SOAP Services Testing Guide

## 🚀 Hướng dẫn nhanh

### 1. Chạy SOAP API Server

```bash
cd DNATestingSystem.SoapAPIServices.TienDM
dotnet run
```

Server sẽ chạy trên:

- HTTPS: `https://localhost:7299`
- HTTP: `http://localhost:5299`

### 2. Chạy Console Client

```bash
cd DNATestingSystem.SoapClient.ConsoleApp.TienDM
dotnet run
```

Hoặc double-click file `run.bat`

**Chọn Protocol:**

- Option 1: HTTPS (Default) - Sử dụng BasicHttpsBinding
- Option 2: HTTP - Sử dụng BasicHttpBinding

## 🛠️ Troubleshooting

### Lỗi: "The provided URI scheme 'https' is invalid; expected 'http'"

**Nguyên nhân:** SOAP client đang sử dụng BasicHttpBinding với HTTPS URL.

**Giải pháp:**

1. Chọn protocol HTTP (option 2) khi chạy Console App
2. Hoặc đảm bảo server hỗ trợ cả HTTP và HTTPS
3. Console App đã được cập nhật để tự động detect và sử dụng binding phù hợp

### Lỗi kết nối

1. Đảm bảo SOAP API server đang chạy
2. Kiểm tra port có đúng không (7299 cho HTTPS, 5299 cho HTTP)
3. Kiểm tra firewall/antivirus

## 📋 Test Scenarios

### Scenario 1: CRUD Operations

1. Chạy Console App
2. Chọn "7. Test All Operations (Demo)"
3. Xem toàn bộ quy trình CRUD

### Scenario 2: Tạo nhiều appointments

1. Chọn "4. Create Multiple Sample Appointments"
2. Nhập số lượng (ví dụ: 5)
3. Xem kết quả tạo

### Scenario 3: Manual Testing

1. Tạo appointment mới (option 3)
2. Xem danh sách (option 1)
3. Cập nhật appointment (option 5)
4. Xóa appointment (option 6)

## 🔧 Postman Testing

### Import Files:

1. `DNATestingSystem-SOAP-Services.postman_collection.json`
2. `DNATestingSystem-Environment.postman_environment.json`

### Test Order:

1. **Get All Appointments** - Kiểm tra service hoạt động
2. **Create Appointment** - Tạo dữ liệu test
3. **Get By ID** - Verify appointment được tạo
4. **Update Appointment** - Cập nhật thông tin
5. **Delete Appointment** - Dọn dẹp dữ liệu

### Sample SOAP Headers:

```
Content-Type: text/xml; charset=utf-8
SOAPAction: http://tempuri.org/IAppointmentsTienDmSoapService/[MethodName]
```

## 🐛 Troubleshooting

### Lỗi thường gặp:

#### 1. "Service not found" / Connection refused

**Nguyên nhân**: SOAP API Server chưa chạy
**Giải pháp**:

```bash
cd DNATestingSystem.SoapAPIServices.TienDM
dotnet run
```

#### 2. SSL Certificate errors

**Nguyên nhân**: Certificate tự ký chưa được trust
**Giải pháp**:

- Option 1: Trust certificate: `dotnet dev-certs https --trust`
- Option 2: Đổi URL thành HTTP trong `Program.cs`:

```csharp
private static readonly string ServiceUrl = "http://localhost:5299/AppointmentsTienDmSoapService.asmx";
```

#### 3. "Invalid operation" khi tạo appointment

**Nguyên nhân**: Database không có dữ liệu reference (User, Service, Status)
**Giải pháp**: Chạy database seeding trước

#### 4. JSON serialization errors

**Nguyên nhân**: Circular references trong models
**Giải pháp**: Đã xử lý với `JsonIgnore` attributes

### Debug Steps:

1. Kiểm tra SOAP API Server logs
2. Verify database connection
3. Check network connectivity
4. Validate SOAP request format

## 📊 Sample Data

### Default Test Data:

```csharp
UserAccountId: 1
ServicesNhanVtid: 1
AppointmentStatusesTienDmid: 1
AppointmentDate: Tomorrow
AppointmentTime: 2 hours from now
SamplingMethod: "Blood Sample"
ContactPhone: "0123456789"
TotalAmount: 500000 VND
```

### Multiple Appointments:

- Sẽ tạo random data với different sampling methods
- Random amounts từ 100,000 - 1,000,000 VND
- Random future dates trong 30 ngày tới

## 🎯 Performance Testing

### Load Testing với Console App:

1. Chọn option 4 (Create Multiple)
2. Nhập số lượng lớn (ví dụ: 100)
3. Monitor performance

### Postman Collection Runner:

1. Chạy collection với iterations
2. Set delays giữa requests
3. Monitor response times

## 📝 API Endpoints

| Method | Endpoint                            | Description                    |
| ------ | ----------------------------------- | ------------------------------ |
| POST   | /AppointmentsTienDmSoapService.asmx | GetAppointmentsTienDmsAsync    |
| POST   | /AppointmentsTienDmSoapService.asmx | GetAppointmentsTienDmByIdAsync |
| POST   | /AppointmentsTienDmSoapService.asmx | CreateAppointmentsTienDmAsync  |
| POST   | /AppointmentsTienDmSoapService.asmx | UpdateAppointmentsTienDmAsync  |
| POST   | /AppointmentsTienDmSoapService.asmx | DeleteAppointmentsTienDmAsync  |

## 🔐 Security Notes

- SOAP services chạy với HTTPS by default
- Không có authentication trong demo này
- Production cần thêm authentication/authorization
- Validate input data trước khi gửi request

## 📞 Support

Nếu gặp vấn đề:

1. Kiểm tra logs trong Console output
2. Verify service endpoints
3. Check database connectivity
4. Review SOAP request format

---

**Tác giả**: TienDM - SE182896  
**Version**: 1.0  
**Last Updated**: July 2025
