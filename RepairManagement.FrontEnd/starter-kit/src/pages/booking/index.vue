<script setup>
import { paginationMeta } from "@/@fake-db/utils";
import { onMounted } from "vue";
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import { VDataTableServer } from "vuetify/labs/VDataTable";
const totalInvoices = ref(0);
const invoices = ref([]);
const selectedRows = ref([]);
const name = ref('')
const email = ref('')

const items = [
  'Item 1',
  'Item 2',
  'Item 3',
  'Item 4',
]
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

const select = ref()
const checkbox = ref(false)
const form = ref()
const props = defineProps({
  contractTypeData: {
    type: Object,
    required: true,
  },
});
const instance = getCurrentInstance();

const isLoading = ref(false);


const refreshData = async () => {
  // await getAllContractType(filterContractType.value);
};

const openDialogUpdateContractType = (id) => {
  // isContractTypeUpdateDialogVisible.value = true;
  // dataId.value = id
}

const onConfirmed = async () => {
  // try{
  //   const result = await ContractTypeService.deleteContractType(contractTypeId.value);
  //   toast(result, {
  //     type: "success",
  //     transition: "flip",
  //     "autoClose": 2000,
  //     theme: "dark",
  //     dangerouslyHTMLString: true,
  //   });
  //   await getAllContractType(filterContractType.value);
  // }catch(error){
  //   toast(error, {
  //     type: "error",
  //     transition: "flip",
  //     "autoClose": 2000,
  //     theme: "dark",
  //     dangerouslyHTMLString: true,
  //   });
  // }
}

const onclickDeleteItem = (id) => {
  contractTypeId.value = id;
  instance?.refs.deleteDialog.show();
}

watchEffect(async () => {

});

onMounted(async () => {

});
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

  <section class="booking mt-8">
        <div class="container-xl">
            <div class="booking__wrapper">
                <h3 class="booking__header">Đặt lịch sửa chữa</h3>

                <div class="booking__content modal__booking">

                    <div class="booking__step active">
                        <div class="booking__step-header">1. Chọn dịch vụ</div>

                        <div class="booking__mess hide">
                            <div class="booking__mess-img" style="background-image: url(./assets/images/booking.jpg)">
                            </div>
                            <p class="booking__mess-text">
                                Mời bạn <span></span> chọn dịch vụ để cửa hàng có thể hỗ trợ chu đáo nhất.
                            </p>
                        </div>

                        <!-- Select Service -->
                        <div class="form-floating modal__booking-select">
                            <select class="form-select" id="selectPosition" aria-label="Floating label select example" fdprocessedid="kf6fc6">
                                <option value="2" selected="">Đặt lịch tại cửa hàng</option>
                                <option value="1">Đặt lịch tại nhà</option>
                            </select>
                            <label for="selectPosition">Dịch vụ</label>
                        </div>

                        <div class="form-floating mbt address-store" data-bs-toggle="modal" data-bs-target="#ModalSearchBranch">
                            <input type="text" placeholder="Chọn cửa hàng sửa chữa" disabled="true" fdprocessedid="e52p1o" data-id="614e8868571b99026b947633" style="color: rgb(0, 0, 0);">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-caret-right-fill" viewBox="0 0 16 16">
                                <path d="m12.14 8.753-5.482 4.796c-.646.566-1.658.106-1.658-.753V3.204a1 1 0 0 1 1.659-.753l5.48 4.796a1 1 0 0 1 0 1.506z"></path>
                            </svg>
                        </div>

                        <div class="form-floating mbt input__booking-address">
                            <input type="text" class="form-control" id="InputAddressModal" placeholder="Nhập địa chỉ khách hàng" fdprocessedid="abh38d">
                            <span id="error-Address" class="text-danger"></span>
                        </div>
                    </div>

                    <div class="booking__step current">
                        <div class="booking__step-header">2. Chọn thời gian</div>

                        <div class="booking__mess">
                            <p class="booking__mess-text">
                                Bạn <span></span> chọn thời gian để cửa hàng được phục vụ nhanh chóng nhất.
                            </p>

                            <div class="booking__mess-img" style="background-image: url('https://linhkienlaptop24h.com/assets/images/booking.jpg')">
                            </div>
                        </div>

                        <!-- Option Time -->
                        <div class="form-floating">
                            <input type="text" autocomplete="off" class="form-control" name="datefilter" id="dateTimeModal" placeholder="Thời gian đặt lịch" fdprocessedid="qukokq">
                            <span id="DateTime-Address" class="text-danger">
                            </span>
                        </div>

                        <!-- Note -->
                        <div class="form-floating mbt">
                            <input type="text" class="form-control" id="InputStatusInput" placeholder="Tình trạng máy" fdprocessedid="w6n1tn">
                            <span id="error-Address" class="text-danger">
                            </span>
                        </div>
                    </div>

                    <div>
                    <VBtn class="primary">Đặt lịch</VBtn>
                    </div>
                </div>
            </div>
        </div>

    </section>
  </div>
</template>

<style lang="scss" scoped>
.booking {
    background-color: #fff;
    margin: auto;
    width: 500px;
}
.booking__wrapper {
    box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
}
.booking__header {
    font-size: 20px;
    font-weight: bold;
    background-color: #efefef;
    text-align: center;
    padding: 20px 0;
}
.booking__content {
    padding: 14px 18px 32px;
}
.booking__content .booking__step:first-child {
    padding: 0 0 8px 20px;
}
.booking__step.active {
    border-left: 2px solid #fc3;
}
.booking__step {
    position: relative;
    padding: 0 0 28px 20px;
    border-left: 2px solid #ddd;
}
.booking__step-header {
    font-size: 18px;
    font-weight: bold;
    padding-bottom: 12px;
}
.booking__content .booking__mess.hide {
    display: none;
}
.booking__mess {
    width: calc(100% + 34px);
    padding: 8px 8px;
    border-radius: 12px;
    display: flex
;
    align-items: center;
    background-color: #f7f4eb;
    border: 1px solid #e3bd4a;
    margin: 0 0 16px -30px;
}
.booking__mess-img {
    min-width: 58px;
    width: 57px;
    height: 57px;
    background-size: cover;
    background-repeat: no-repeat;
    background-position: top center;
    border-radius: 12px;
    box-shadow: rgba(0, 0, 0, 0.12) 0px 1px 3px, rgba(0, 0, 0, 0.24) 0px 1px 2px;
}
.booking__content .booking__step:first-child .booking__mess .booking__mess-text {
    margin: 0 6px 0 16px;
}
.booking__mess-text {
    width: 84%;
    font-size: 15px;
    line-height: 20px;
    margin: 0 16px 0 10px;
}
.booking .modal__booking .form-floating {
    margin-bottom: 0;
}
.modal__booking .form-floating {
    margin-bottom: 16px;
    border-radius: 5px;
}
.form-floating {
    position: relative;
}
.modal__booking .form-floating>.form-select {
    padding: 22px 22px 4px 12px;
    border-radius: 5px;
}
.modal__booking .modal__booking-select select {
    min-height: 54px;
    font-size: 16px;
}
.form-floating>.form-select {
    padding-top: 1.625rem;
    padding-bottom: .625rem;
}
.form-floating>.form-control, .form-floating>.form-select {
    height: calc(3.5rem + 2px);
    line-height: 1.25;
}
.form-select {
    display: block;
    width: 100%;
    padding: .375rem 2.25rem .375rem .75rem;
    -moz-padding-start: calc(0.75rem - 3px);
    font-size: 1rem;
    font-weight: 400;
    line-height: 1.5;
    color: #212529;
    background-color: #fff;
    background-image: url('http://www.w3.org/2000/svg');
    background-repeat: no-repeat;
    background-position: right .75rem center;
    background-size: 16px 12px;
    border: 1px solid #ced4da;
    border-radius: .25rem;
    transition: border-color .15s ease-in-out, box-shadow .15s ease-in-out;
    -webkit-appearance: none;
    -moz-appearance: none;
    appearance: none;
}
.form-floating>.form-control:focus~label, .form-floating>.form-control:not(:placeholder-shown)~label, .form-floating>.form-select~label {
    transform: scale(.85) translateY(-0.79rem) translateX(0.15rem);
}
.modal__booking .form-floating label {
    padding: 13px 12px !important;
    font-size: 16px;
}
.form-floating>.form-control:focus~label, .form-floating>.form-control:not(:placeholder-shown)~label, .form-floating>.form-select~label {
    opacity: .65;
    transform: scale(.85) translateY(-.5rem) translateX(.15rem);
}
.form-floating>label {
    position: absolute;
    top: 0;
    left: 0;
    height: 100%;
    padding: 1rem .75rem;
    pointer-events: none;
    border: 1px solid transparent;
    transform-origin: 0 0;
    transition: opacity .1s ease-in-out, transform .1s ease-in-out;
}
.booking__step .form-floating.mbt {
    margin-top: 10px;
    margin-bottom: 22px;
}
.booking__step .form-floating.mbt {
    margin-top: 10px;
    margin-bottom: 22px;
}
.booking .modal__booking .form-floating {
    margin-bottom: 0;
}
.modal__booking .form-floating.mbt {
    margin-bottom: 1px;
}
.modal__booking .address-store {
    margin-top: 16px !important;
    border: 1px solid #ddd;
    padding: 12px 12px;
    cursor: pointer;
    color: #aeaeae;
    font-size: 16px;
    display: flex
;
    align-items: center;
    justify-content: space-between;
}
.modal__booking .form-floating {
    margin-bottom: 16px;
    border-radius: 5px;
}
.form-floating {
    position: relative;
}
.modal__booking .address-store>input:disabled {
    background-color: #fff;
    border: 0;
}
.modal__booking .address-store>input {
    height: 26px;
    cursor: pointer;
    background-color: #fff;
    width: auto;
    margin-right: 6px;
}
.modal__booking .form-floating input {
    font-size: 16px;
    line-height: 20px;
    height: 100%;
    border-radius: 5px;
    height: 54px;
}
.modal__booking .form-floating>.form-control:focus, .form-floating>.form-control:not(:placeholder-shown) {
    padding: 26px 12px 6px;
}
.form-control {
    display: block;
    width: 100%;
    padding: .375rem .75rem;
    font-size: 1rem;
    font-weight: 400;
    line-height: 1.5;
    color: #212529;
    background-color: #fff;
    background-clip: padding-box;
    border: 1px solid #ced4da;
    -webkit-appearance: none;
    -moz-appearance: none;
    appearance: none;
    border-radius: .25rem;
    transition: border-color .15s ease-in-out, box-shadow .15s ease-in-out;
}
.input__booking-address #error-Address {
    margin: 2px 0 0 10px;
    font-size: 15px;
    display: block;
    min-height: 22px;
}
.text-danger {
    --bs-text-opacity: 1;
    color: rgba(var(--bs-danger-rgb), var(--bs-text-opacity)) !important;
}
.booking__step.active::before {
    background: #fc3;
}

.booking__step::before {
    position: absolute;
    content: '';
    width: 20px;
    height: 20px;
    border-radius: 10px;
    top: 0px;
    left: -11px;
    background-color: #ddd;
}
</style>
