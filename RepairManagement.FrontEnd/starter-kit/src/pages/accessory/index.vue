<script setup>
import { paginationMeta } from "@/@fake-db/utils";
import { onMounted } from "vue";
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import { VDataTableServer } from "vuetify/labs/VDataTable";

const totalInvoices = ref(0);
const invoices = ref([]);
const selectedRows = ref([]);
const isLoading = ref(false);
const currentPage = ref(1);
const products = ref([
  {
    id: 1,
    imageUrl: "https://hethong.24hlaptop.com/image_product/bo-ve-sinh-1706526195942.png",
    name: "Bộ vệ sinh laptop Melon (4PC)",
    price: "49.000₫",
  },
  {
    id: 2,
    imageUrl:
      "https://hethong.24hlaptop.com/image_product/1t-2_7ffbe1ffb8ea4f91944cc6a19c908013_master-1727773341680.webp",
    name: "Ổ cứng SSD Kingston 1TB M2 2280 NVME PCIE NV3",
    price: "1.890.000₫",
  },
  {
    id: 3,
    imageUrl:
      "https://hethong.24hlaptop.com/image_product/919d5259d3bfda576a83e9bc791056fb1-1645768714551-1-1645769083707-1-1645769149623-1-1645769318522-.jpg",
    name: "Ram laptop Samsung 8GB DDR3L 1600",
    price: "400.000₫",
  },
  {
    id: 4,
    imageUrl:
      "https://hethong.24hlaptop.com/image_product/PIN-LAPTOP-SURFACE-BOOK1-1634644714407-.jpg",
    name: "Pin dùng cho laptop Surface Book 1 (G3HTA020H)",
    price: "1.500.000₫",
  },
  {
    id: 5,
    imageUrl:
      "https://hethong.24hlaptop.com/image_product/kaspersky-internetsecurity-5pc_1633052976-1656928563535.jpg",
    name: "Phần mềm diệt virus Kaspersky Internet Security",
    price: "300.000₫",
  },
  {
    id: 6,
    imageUrl:
      "https://hethong.24hlaptop.com/image_product/SAC-LAPTOP-DELL-19.5V-%204.62A-11-1634826991630-.jpg",
    name: "Sạc dùng cho laptop Dell 19.5V - 4.62A chân kim to",
    price: "250.000₫",
  },
]);
const listBanner = ref([
  {
    id: 1,
    link:
      "https://suachualaptop24h.com/images/slideshow/2020/06/12/compress2/slide_sc1_1591958105.jpg.webp",
  },
  {
    id: 2,
    link:
      "https://suachualaptop24h.com/images/slideshow/2024/04/24/compress2/dich-vu-sua-chua-laptop_1713929666.jpg.webp",
  },
]);
const instance = getCurrentInstance();
const options = ref({
  page: 1,
  itemsPerPage: 10,
  sortBy: [],
  groupBy: [],
  search: undefined,
});
currentPage.value = options.value.page;
const paginatedData = computed(() => {
  const start = (options.value.page - 1) * options.value.itemsPerPage;
  const end = start + options.value.itemsPerPage;
  return invoices.value.slice(start, end);
});
const totalPages = computed(() => {
  return Math.ceil((invoices.value.length * 1.0) / options.value.itemsPerPage);
});

const formatDate = (dateString) => {
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");
  return `${year}-${month}-${day}`;
};
watchEffect(async () => {});

onMounted(async () => {});
</script>

<template>
  <div>
    <VCard class="slider">
      <VCarousel
        show-arrows="hover"
        hide-delimiters
        continuous="true"
        cycle
        interval="2000"
      >
        <VCarouselItem v-for="b in listBanner" :key="b.id" :src="b.link" cover>
        </VCarouselItem>
      </VCarousel>
    </VCard>
    <VCard class="product-list-home category__trend mb-4" id="">
      <div class="category__trend-header">
        <h2>Danh mục sản phẩm</h2>
      </div>

      <div class="catalog__list">
        <a
          class="catalog__item"
          href="https://linhkienlaptop24h.com/danh-muc/disk-o-cung"
        >
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/o-cung-1649472741655.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Disk - Ổ cứng</p>
        </a>

        <a class="catalog__item" href="https://linhkienlaptop24h.com/danh-muc/man-hinh">
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/man-hinh-laptop-1649472448268.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">LCD - Màn hình</p>
        </a>

        <a class="catalog__item" href="https://linhkienlaptop24h.com/danh-muc/pin-laptop">
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/pin-laptop-1649472476770.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Battery - Pin laptop</p>
        </a>

        <a class="catalog__item" href="https://linhkienlaptop24h.com/danh-muc/vo">
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/vo-laptop-1664423002648.png"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Vỏ laptop</p>
        </a>

        <a class="catalog__item" href="https://linhkienlaptop24h.com/danh-muc/cap">
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/cap-man-hinh-1661157942007.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Cáp màn hình laptop</p>
        </a>

        <a
          class="catalog__item"
          href="https://linhkienlaptop24h.com/danh-muc/fan-laptop-quat-tan-nhiet"
        >
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/quat-tan-nhiet-laptop-1649472512289.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Quạt tản nhiệt</p>
        </a>

        <a
          class="catalog__item"
          href="https://linhkienlaptop24h.com/danh-muc/linh-kien-laptop-mobile-tablet"
        >
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/63(4)-02-1653451591159.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Linh kiện</p>
        </a>

        <a class="catalog__item" href="https://linhkienlaptop24h.com/danh-muc/chuot">
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/chuot-1651825322388.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Chuột</p>
        </a>

        <a
          class="catalog__item"
          href="https://linhkienlaptop24h.com/danh-muc/pin-dien-thoai"
        >
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/pin đt-1662953970235.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Pin điện thoại</p>
        </a>

        <a
          class="catalog__item"
          href="https://linhkienlaptop24h.com/danh-muc/man-hinh-dien-thoai"
        >
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/màn hình đt-1662954276806.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Màn hình điện thoại</p>
        </a>

        <a
          class="catalog__item"
          href="https://linhkienlaptop24h.com/danh-muc/mainboard-dien-thoai"
        >
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/mainbroad điện thoại-1662953676067.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Mainboard điện thoại</p>
        </a>

        <a class="catalog__item" href="https://linhkienlaptop24h.com/danh-muc/ram">
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/ram-laptop-1649472760305.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">RAM - Bộ nhớ trong</p>
        </a>

        <a
          class="catalog__item"
          href="https://linhkienlaptop24h.com/danh-muc/keyboard-laptop"
        >
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/ban-phim-laptop-1649472955716.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Keyboard - Laptop</p>
        </a>

        <a
          class="catalog__item"
          href="https://linhkienlaptop24h.com/danh-muc/adapter-sac-laptop"
        >
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/sac-laptop-1649472773766.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Adapter - Sạc laptop</p>
        </a>

        <a class="catalog__item" href="https://linhkienlaptop24h.com/danh-muc/loa-laptop">
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/Loa-laptop-1661157314584.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Loa laptop</p>
        </a>

        <a class="catalog__item" href="https://linhkienlaptop24h.com/danh-muc/ban-le">
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/ban-le-laptop-1661158158767.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Bản lề Laptop</p>
        </a>

        <a class="catalog__item" href="https://linhkienlaptop24h.com/danh-muc/phan-mem">
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/software-1656935323193.png"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Phần mềm bản quyền</p>
        </a>

        <a
          class="catalog__item"
          href="https://linhkienlaptop24h.com/danh-muc/phu-kien-laptop-tablet-mobile"
        >
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/phu-kien-1651825202526.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Phụ kiện</p>
        </a>

        <a
          class="catalog__item"
          href="https://linhkienlaptop24h.com/danh-muc/pin-surface"
        >
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/pin-suface-1653477059417.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Pin Surface</p>
        </a>

        <a
          class="catalog__item"
          href="https://linhkienlaptop24h.com/danh-muc/pin-macbook"
        >
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/pin-macbook-1653476980185.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Pin Macbook</p>
        </a>

        <a
          class="catalog__item"
          href="https://linhkienlaptop24h.com/danh-muc/man-hinh-macbook"
        >
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/man-hinh-macbook-1666085821462.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Màn hình MacBook</p>
        </a>

        <a
          class="catalog__item"
          href="https://linhkienlaptop24h.com/danh-muc/o-cung-macbook"
        >
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/2-03-1684898903110.png"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Ổ cứng Macbook</p>
        </a>

        <a
          class="catalog__item"
          href="https://linhkienlaptop24h.com/danh-muc/pin-may-tinh-bang"
        >
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/pin mt bảng-1662955063272.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Pin máy tính bảng</p>
        </a>

        <a
          class="catalog__item"
          href="https://linhkienlaptop24h.com/danh-muc/man-hinh-may-tinh-bang"
        >
          <img
            src="https://hethong.24hlaptop.com/image_menu_website/màn hình mt bảng-1662955197079.jpg"
            alt="img category"
            class="catalog__item-img"
            loading="lazy"
          />
          <p class="catalog__item-text">Màn hình máy tính bảng</p>
        </a>
      </div>
    </VCard>

    <VCard class="product-list-home category-home mb-3" id="">
      <div class="container" style="padding: 20px 40px">
        <div class="title-category">
          <a href="https://linhkienlaptop24h.com/tim-kiem?"
            ><h2>Sản phẩm hot trong ngày</h2></a
          >
          <div class="sub_cat_title"></div>
        </div>
        <div class="clear-both"></div>
        <div
          class="product-list product-cat-top list-product-home-2022-thuan"
          id="div__product_home"
        >
          <div
            class="swiper-slide irt-item-proc item-proc__irt"
            v-for="item in products"
            :key="item.id"
          >
            <div class="irt-container">
              <a href="https://linhkienlaptop24h.com/san-pham/bo-ve-sinh-laptop-melon">
                <div class="irt-img">
                  <img :src="item.imageUrl" class="irt-img__main" />
                </div>

                <span class="irt-price">
                  <span class="price-border">{{ item.price }}</span>
                  <span class="price-shadow">{{ item.price }}</span>
                </span>

                <div
                  href="https://linhkienlaptop24h.com/san-pham/bo-ve-sinh-laptop-melon"
                  class="irt-name"
                >
                  {{ item.name }}
                </div>
              </a>
            </div>

            <div class="info-show-all-pc">
              <div class="irt_info-proc">
                <div class="title-hover">{{ item.name }}</div>
                <div class="desc-hover">
                  <ul class="irt__info">
                    <li>
                      - Giá bán: <b style="color: red">{{ item.name }}</b>
                    </li>
                    <li>- Bảo hành: 0 tháng</li>
                  </ul>
                </div>

                <div class="title-hover">
                  <img
                    src="https://linhkienlaptop24h.com/assets/images/information-buttonw.png"
                    width="18px"
                  />Thông số kỹ thuật
                </div>
                <div class="desc-hover">
                  <ul class="irt__info">
                    <li>- Thương hiệu: OEM</li>
                    <li>- Dòng máy: Laptop, Macbook</li>
                  </ul>
                </div>

                <div class="title-hover-sale">
                  <img
                    src="https://linhkienlaptop24h.com/assets/images/giftboxw.png"
                    width="18px"
                  />Khuyến mãi
                </div>
                <div class="desc-hover-sale">
                  <ul class="list-promotions">
                    <li>- Miễn phí 100% phí lắp đặt và cài đặt</li>
                    <li>- Giảm 50% phí bảo dưỡng, cài đặt</li>
                    <li>- Giảm 5% phí linh kiện cho lần mua hàng tiếp theo</li>
                    <li>- Tặng thẻ ưu đãi trị giá 500k</li>
                  </ul>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </VCard>
  </div>
</template>

<style lang="scss">
#invoice-list {
  .invoice-list-actions {
    inline-size: 8rem;
  }

  .invoice-list-filter {
    inline-size: 12rem;
  }
}
.calendar-date-picker {
  display: none;

  + .flatpickr-input {
    + .flatpickr-calendar.inline {
      border: none;
      box-shadow: none;

      .flatpickr-months {
        border-block-end: none;
      }
    }
  }

  & ~ .flatpickr-calendar .flatpickr-weekdays {
    margin-block: 0 4px;
  }
}
.product-list-home.category__trend {
  min-height: auto;
  margin-bottom: 32px !important;
  border: 1px solid #eee;
  padding: 0 40px;
  border-radius: 6px;
  margin-top: 20px;
}
.product-list-home {
  background-color: #fff;
  margin-top: 4px;
  min-height: 300px;
}
.category__trend-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 18px 0;
}
.product-list-home.category__trend .catalog__list {
  border-top: 1px solid #ddd;
  display: flex;
  flex-wrap: wrap;
  align-items: flex-start;
  justify-content: center;
}
.product-list-home .catalog__item {
  /* border-bottom: 1px solid #eee; */
  /* padding: 18px 20px; */
  padding: 6px 12px;
  display: flex;
  align-items: center;
  /* justify-content: center; */
  justify-content: flex-start;
  flex-direction: column;
  width: calc(100% / 11);
}
.product-list-home .catalog__item > img {
  /* width: 120px; */
  /* max-height: 120px; */
  /* height: 74px; */
  height: 60px;
  margin: 0 auto;
}
.product-list-home .catalog__item .catalog__item-text {
  display: -webkit-box;
  font-size: 13px;
  line-height: 1.2;
  min-height: 54px;
  color: black;
  -webkit-box-orient: vertical;
  -webkit-line-clamp: 2;
  text-align: center;
  overflow: hidden;
}
.catalog__btn {
  font-size: 1.6rem;
  padding: 6px 60px;
  display: block;
  margin: 20px auto 0;
}
.product-list-home .title-category,
.title-block-common,
.related-product .box-title {
  overflow: hidden;
  border-bottom: solid 2px #0680c2;
  height: 40px;
  line-height: 40px;
  margin-bottom: 5px;
  background-color: #fff;
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.product-list-home .sub_cat_title {
  font-size: 1.6rem;
}
.product-list-home .sub_cat_title {
  float: left;
  line-height: 15px;
  /* margin-top: 14px; */
}
.product-list-home .viewall,
.title-block-common .viewall,
.related-product .viewall {
  float: right;
  margin-right: 20px;
}
.clear-both {
  clear: both;
}
.product-list {
  overflow: hidden;
  background: #fff;
}
.irt-item-proc {
  overflow: hidden;
  float: left;
  width: calc(100% / 6);
  position: relative;
  opacity: 1;
  transition: all 0.5s ease;
  border-bottom: solid 1px #eee;
  border-right: solid 1px #eee;
  border-left: solid 1px #eee;
  font-size: 13px;
}
.swiper-slide {
  -webkit-flex-shrink: 0;
  -ms-flex-negative: 0;
  flex-shrink: 0;
  width: 33%;
  height: 100%;
  position: relative;
  -webkit-transition-property: -webkit-transform;
  transition-property: -webkit-transform;
  -o-transition-property: transform;
  transition-property: transform;
  transition-property: transform, -webkit-transform;
}
.irt-container {
  padding: 10px;
  overflow: hidden;
}
.irt-container:hover .info-show-all-pc {
  display: block;
  z-index: 10;
}
.info-show-all-pc {
  width: 100px;
  height: 100px;
  z-index: 10;

  display: none;
}
.irt-img {
  width: 100%;
  display: block;
  position: relative;
  padding-top: 100%;
  margin-bottom: 5px;
}
.irt-img__main {
  position: absolute;
  width: 100%;
  height: 100%;
  object-fit: contain;
  object-position: center;
  left: 0;
  top: 0;
  display: block;
  margin: auto;
}
.product-list-home .irt-img .irt-img__sale,
.product-list-home .irt-img .irt-img__hot {
  top: 18px;
}
.irt-img__hot {
  width: 35px !important;
  position: absolute;
  left: -2px;
  top: -2px;
}
img {
  max-width: 100%;
  height: auto;
  border: 0;
  vertical-align: top;
}
.product-list-home .category-home {
  padding: 0 40px;
}
.price-border {
  text-shadow: 3px 0 0 #ffffff, -3px 0 0 #fff, 0 3px 0 #fff, 0 -3px 0 #fff, 2px 2px #fff,
    -2px -2px 0 #fff, 2px -2px 0 #fff, -2px 2px 0 #fff;
  position: relative;
  top: 0;
  left: 0;
  z-index: 2;
  display: block;
}
.irt-price,
.irt-price-full {
  font-weight: bold;
  color: #c60000;
  display: block;
  line-height: 16px;
  font-family: "Oswald", sans-serif;
  font-size: 22px;
  font-style: italic;
  margin-top: 14px;
  position: relative;
  white-space: nowrap;
}
.irt-name {
  color: #333;
  display: block;
  line-height: 16px;
  height: 32px;
  overflow: hidden;
  margin: 12px 0 8px 0;
  font-size: 16px;
  font-weight: bold;
}
.price-shadow {
  position: absolute !important;
  top: 0;
  left: 0;
  text-shadow: 0 0 6px #000000;
  z-index: 1;
  display: block;
}
.img-prod {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
}
.image-container {
  width: 100%;
  padding-top: 75%; /* 4:3 aspect ratio (adjust as needed) */
  position: relative;
  overflow: hidden;
}
.service {
  padding: 26px 0;
}
@media (min-width: 1400px) {
  .container,
  .container-lg,
  .container-md,
  .container-sm,
  .container-xl,
  .container-xxl {
    max-width: 1320px;
  }
}
.service__content {
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 24px 0;
}
.service__label {
  background-color: #0088de;
  height: 54px;
  width: 54px;
  display: flex;
  justify-content: center;
  align-items: center;
  transform: rotate(45deg);
  border-radius: 6px;
}
.service__label-img {
  transform: rotate(-45deg);
  height: 35px;
  width: 35px;
}
.service__details {
  margin-left: 18px;
}
.service__details-name {
  color: #444444;
  font-size: 15px;
  font-weight: 600;
}
.service__details-text {
  font-size: 14px;
}
.row {
  --bs-gutter-x: 1.5rem;
  --bs-gutter-y: 0;
  display: flex;
  flex-wrap: wrap;
  margin-top: calc(-1 * var(--bs-gutter-y));
  margin-right: calc(-0.5 * var(--bs-gutter-x));
  margin-left: calc(-0.5 * var(--bs-gutter-x));
}
</style>
