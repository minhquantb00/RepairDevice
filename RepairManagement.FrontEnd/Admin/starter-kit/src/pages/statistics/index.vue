<script setup>
import { Bar } from "vue-chartjs";

import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale,
} from "chart.js";
import { ref, onMounted, watch } from "vue";
import { DeviceApi } from "@/apis/device/deviceApi";
ChartJS.register(
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale
);
let chartInstance = null;
const chartData = ref({
  labels: [],
  datasets: [
    {
      data: [],
    },
  ],
});

const chartOptions = ref({
  responsive: true,
  plugins: {
    legend: {
      display: true,
      position: "top",
    },
    tooltip: {
      enabled: true,
    },
  },
  scales: {
    x: {
      beginAtZero: true,
    },
    y: {
      beginAtZero: true,
    },
  },
});

const getStatistics = async () => {
  const result = await DeviceApi.getStatistics();
  console.log(result);
  const labels = Array.from({ length: 12 }, (_, i) => `Tháng ${i + 1}`);
  chartData.value.labels.push(...labels);
  const doanhSoTrenMonth = Array(12).fill(0);
  result.data.forEach(({ thang, doanhSo }) => {
    doanhSoTrenMonth[thang - 1] = doanhSo;
  });
  chartData.value = {
    labels: labels,
    datasets: [
      {
        label: "Doanh số công ty theo tháng",
        data: doanhSoTrenMonth,
        backgroundColor: "rgba(75, 192, 192, 0.2)",
        borderColor: "rgba(75, 192, 192, 1)",
        borderWidth: 1,
      },
    ],
  };
  chartData.value.datasets[0].data = [...doanhSoTrenMonth];
};
watch(
  () => chartData.value.datasets[0].data,
  () => {
    if (chartInstance) {
      chartInstance.update();
    }
  }
);

onMounted(async () => {
  await getStatistics();
  console.log(chartData.value);
});
</script>

<template>
  <div>
    <Bar id="my-chart-id" :options="chartOptions" :data="chartData" />
  </div>
</template>
