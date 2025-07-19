# 🧪 Quick Testing Guide

## Console App Test Commands

```bash
# Quick connection test
cd DNATestingSystem.SoapClient.ConsoleApp.TienDM
dotnet run -- --test

# Interactive mode
dotnet run
```

## Expected Output

### ✅ Successful Connection

```
===========================================
    DNA Testing System SOAP Client
===========================================
Running simple SOAP test...
Testing connection to: https://localhost:7077/AppointmentsTienDmSoapService.asmx
✓ Connected successfully!
⏳ Calling GetAllAppointments...
✓ Retrieved 15 appointments

First 3 appointments:
  ID: 1
  Address: 123 Test Street, Test City
  Phone: +1234567890
  Amount: $399.99
  Paid: False
  ---
  ID: 2
  Address: 123 Main St, City
  Phone: 0904567890
  Amount: $650.00
  Paid: False
  ---

✓ Test completed successfully!
```

### ❌ Connection Error

```
❌ Error: No connection could be made because the target machine actively refused it
```

**Solution:** Ensure SOAP API server is running on port 7077

## Postman Testing

### Variables

- `baseUrl`: https://localhost:7077
- `baseUrlHttp`: http://localhost:5077
- `appointmentId`: 1

### Test Sequence

1. **Get All Appointments** - Verify service is working
2. **Create Appointment** - Test creation
3. **Get Appointment By ID** - Verify created appointment
4. **Update Appointment** - Test modification
5. **Delete Appointment** - Test deletion

## Architecture Summary

```
SOAP Client (Console App)
    ↓ HTTPS/HTTP
SOAP API Service (Port 7077/5077)
    ↓ Entity Framework
SQL Server Database
```

## Key Fixed Issues

✅ **BasicHttpsBinding for HTTPS**
✅ **DateTime? for nullable dates**  
✅ **TimeSpan? for time values**
✅ **Automatic protocol detection**
✅ **Comprehensive error handling**
