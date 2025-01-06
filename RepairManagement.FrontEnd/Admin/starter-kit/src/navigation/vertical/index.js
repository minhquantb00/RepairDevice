import { RoleEnum } from "@/enums/enumerate";

const userInfo = JSON.parse(localStorage.getItem("userInfo"));

const userRoles = userInfo?.Permission || [];

const menu = [
  {
    title: "Quản lý khách hàng",
    to: { path: "customer" },
    icon: { icon: "tabler-brand-teams" },
  },
  {
    title: "Quản lý loại thiết bị",
    to: { path: "contract" },
    icon: { icon: "tabler-packages" },
  },
  {
    title: "Sửa chữa thiết bị",
    to: { path: "customer-device" },
    icon: { icon: "tabler-settings-spark" },
  },
  {
    title: "Phân công nhân sự",
    to: { path: "phan-cong" },
    Permission: [RoleEnum.Admin],
    icon: { icon: "tabler-git-branch" },
  },
  {
    title: "Việc của tôi",
    to: { path: "phancong-nhanvien" },
    icon: { icon: "tabler-building-skyscraper" },
  },
  {
    title: "Quản lý linh kiện",
    to: { path: "linh-kien" },
    icon: { icon: "tabler-access-point" },
  },
  {
    title: "Quản lý nhân viên",
    to: { path: "nhan-vien" },
    Permission: [RoleEnum.Admin],
    icon: { icon: "tabler-user-check" },
  },
  {
    title: "Thống kê doanh số",
    to: {path: "statistics"},
    Permission: [RoleEnum.Admin],
    icon: {icon: "tabler-user-check"}
  }
];

const filteredMenu = menu.filter((item) => {
  if (!item.Permission) return true;
  return item.Permission.some((requiredRole) => userRoles.includes(requiredRole));
});

export default filteredMenu;
