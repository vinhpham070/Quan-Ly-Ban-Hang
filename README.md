# QuanLyBanHang (Sales Management System / Hệ Thống Quản Lý Bán Hàng)

**EN:** A desktop/web application designed to streamline and automate daily sales operations, inventory tracking, customer management, and revenue reporting.  
**VI:** Ứng dụng desktop/web được thiết kế nhằm tối ưu hóa và tự động hóa quy trình bán hàng hằng ngày, theo dõi tồn kho, quản lý khách hàng và báo cáo doanh thu.

---

## Table of Contents / Mục lục
- [Features / Tính năng chính](#features--tính-năng-chính)
- [Tech Stack / Công nghệ sử dụng](#tech-stack--công-nghệ-sử-dụng)
- [Prerequisites / Yêu cầu hệ thống](#prerequisites--yêu-cầu-hệ-thống)
- [Installation & Setup / Cài đặt & Khởi chạy](#installation--setup--cài-đặt--khởi-chạy)
- [Database Configuration / Cấu hình cơ sở dữ liệu](#database-configuration--cấu-hình-cơ-sở-dữ-liệu)
- [Project Structure / Cấu trúc thư mục](#project-structure--cấu-trúc-thư-mục)
- [Screenshots / Hình ảnh minh họa](#screenshots--hình-ảnh-minh-họa)
- [License / Giấy phép](#license--giấy-phép)

---

## Features / Tính năng chính

- **Authentication & Authorization / Xác thực & Phân quyền:**  
  - **EN:** Secure login system with role-based access control (Admin, Cashier/Staff).  
  - **VI:** Hệ thống đăng nhập bảo mật với phân quyền theo vai trò (Quản trị viên, Thu ngân/Nhân viên).
- **Product & Inventory Management / Quản lý Sản phẩm & Tồn kho:**  
  - **EN:** Add, update, delete, and search products; track real-time stock levels and receive low-stock alerts.  
  - **VI:** Thêm, sửa, xóa và tìm kiếm sản phẩm; theo dõi số lượng tồn kho theo thời gian thực và cảnh báo sắp hết hàng.
- **Sales & Invoicing / Bán hàng & Xuất hóa đơn:**  
  - **EN:** Fast order processing, barcode/product code lookup, discount calculation, and printable invoice generation.  
  - **VI:** Xử lý đơn hàng nhanh chóng, tra cứu mã vạch/mã sản phẩm, tính chiết khấu và xuất hóa đơn in ấn.
- **Customer & Supplier Management / Quản lý Khách hàng & Nhà cung cấp:**  
  - **EN:** Maintain customer profiles, purchase history, and supplier records.  
  - **VI:** Lưu trữ thông tin khách hàng, lịch sử mua hàng và hồ sơ nhà cung cấp.
- **Reporting & Analytics / Báo cáo & Thống kê:**  
  - **EN:** Visual dashboards and exportable reports for daily, monthly, and yearly revenue, best-selling items, and profit margins.  
  - **VI:** Biểu đồ trực quan và xuất báo cáo doanh thu theo ngày/tháng/năm, sản phẩm bán chạy và biên lợi nhuận.

---

## Tech Stack / Công nghệ sử dụng

- **Language / Ngôn ngữ:** C# (.NET 8 / .NET 10)
- **UI Framework / Giao diện:** Windows Forms (WinForms) / WPF *(update based on your project / cập nhật theo dự án của bạn)*
- **Database / Cơ sở dữ liệu:** SQLite
- **ORM / Data Access / Truy xuất dữ liệu:** Entity Framework Core (`Microsoft.EntityFrameworkCore.Sqlite`) / `Microsoft.Data.Sqlite`
- **Reporting / Báo cáo:** ClosedXML / RDLC *(if applicable / nếu có)*
- **IDE:** Visual Studio 2022

---

## Prerequisites / Yêu cầu hệ thống

- [Visual Studio 2022](https://visualstudio.microsoft.com/) (with **.NET desktop development** workload / kèm gói **Phát triển ứng dụng desktop .NET**)
- .NET SDK (version `[e.g., 8.0 / 10.0]`)
- [DB Browser for SQLite](https://sqlitebrowser.org/) *(optional, for viewing/editing the `.db` file / tùy chọn, dùng để xem và chỉnh sửa file `.db`)*

---

## Installation & Setup / Cài đặt & Khởi chạy

1. **Clone the repository / Tải mã nguồn về máy:**
   ```bash
   git clone [https://github.com/vinhpham070/Quan-Ly-Ban-Hang.git](https://github.com/vinhpham070/Quan-Ly-Ban-Hang.git)
   cd Quan-Ly-Ban-Hang
