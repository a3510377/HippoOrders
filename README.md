# HippoOrders

一個使用 C# 製作的破軟體，幫朋友寫的功課

## 先決條件

- Windows
- Visual Studio

## 快速開始

### 在 Visual Studio 中開啟

1. 以 Visual Studio 開啟 `HippoOrders.sln`
2. 將 `HippoOrders` 設為啟動專案
3. 按 F5 進行建置並執行

## 專案結構

- `HippoOrders/` – 主要 WinForms 專案
  - `Forms/` – UI 表單，例如 `FormAddOrder`、`FormListOrder`、`FormListGods`
  - `Component/` – 可重用的產品／購物車展示使用者控制項
  - `Models/` – `OrderModel`、`OrderItemDetailModel`
  - `DbInitializer.cs` – 資料庫初始化／種子資料
  - `RootMenu.cs` – 主選單表單
  - `Properties/` – 資源與設定
