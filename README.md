# Hệ Thống Quản Lý Bán Hàng (POS System)

Ứng dụng desktop hỗ trợ quản lý bán hàng tại quầy dành cho quán cà phê / nhà hàng, được xây dựng trên nền tảng **WPF (Windows Presentation Foundation)** và **C# (.NET)**.

---

## Tính năng chính

* **Quản lý bàn trực quan:** Hiển thị danh sách bàn, tìm kiếm nhanh theo tên bàn và theo dõi trạng thái bàn đang phục vụ.
* **Quản lý thực đơn (Món & Nước):** Danh sách món ăn/đồ uống kèm đơn giá rõ ràng, tích hợp thanh tìm kiếm tức thì.
* **Xử lý hóa đơn (Order & Billing):**
  * Tự động cập nhật hóa đơn tương ứng khi chọn từng bàn.
  * Thêm món từ thực đơn vào hóa đơn, điều chỉnh trực tiếp số lượng ngay trên bảng (`DataGrid`) hoặc xóa món.
  * Tự động tính tổng thành tiền theo thời gian thực.
  * Thanh toán và lưu hóa đơn vào cơ sở dữ liệu.

---

## Công nghệ sử dụng

* **Ngôn ngữ:** C#
* **Giao diện (UI):** WPF (XAML) với giao diện 3 cột (Danh sách bàn – Thực đơn – Hóa đơn).
* **Cơ sở dữ liệu:** SQLite (thư viện `Microsoft.Data.Sqlite` hoặc `System.Data.SQLite`).
* **IDE:** Visual Studio 2022

---

## Hướng dẫn cài đặt và chạy dự án

### 1. Yêu cầu hệ thống
* Hệ điều hành Windows 10/11.
* [.NET SDK](https://dotnet.microsoft.com/download) (phiên bản .NET 6.0 / .NET 8.0 hoặc .NET Framework 4.8 tùy cấu hình project).
* Visual Studio 2022 (đã cài đặt workload **.NET desktop development**).

### 2. Cấu hình Cơ sở dữ liệu (SQLite)
Ứng dụng sử dụng SQLite nên không cần cài đặt thêm hệ quản trị cơ sở dữ liệu bên ngoài:

1. Đảm bảo file cơ sở dữ liệu `QuanLyBanHang.db` *(sửa lại tên file `.db` thực tế của bạn)* nằm trong thư mục dự án và đã được thiết lập thuộc tính **Copy to Output Directory = Copy if newer** (hoặc **Copy always**) trong Visual Studio.
2. Kiểm tra chuỗi kết nối trong project (ví dụ trong `App.config` hoặc lớp kết nối database):
   ```xml
   <connectionStrings>
       <add name="DefaultConnection" 
            connectionString="Data Source=QuanLyBanHang.db;Version=3;" />
   </connectionStrings>
