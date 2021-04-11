import { ChartType } from './chart.model';

const productStatusChart: ChartType = {
  chart: {
    height: 320,
    type: 'pie',
  },
  series: [45, 5, 12],
  labels: ['Ativos', 'Inativos', 'Pendentes'],
  colors: ['#3bafda', '#d1dee4', '#f672a7'],
  legend: {
    show: true,
    position: 'bottom',
    horizontalAlign: 'center',
    verticalAlign: 'middle',
    floating: false,
    fontSize: '14px',
    offsetX: 0,
    offsetY: -10
  },
  dataLabels: {
    enabled: false
  },
  responsive: [{
    breakpoint: 600,
    options: {
      chart: {
        height: 240
      },
      legend: {
        show: false
      },
    }
  }]
};

const alertReadingStatusChart: ChartType = {
  chart: {
    height: 320,
    type: 'pie',
  },
  series: [45, 12],
  labels: ['Lidos', 'Não lidos'],
  colors: ['#3bafda',  '#FFA7B6'],
  legend: {
    show: true,
    position: 'bottom',
    horizontalAlign: 'center',
    verticalAlign: 'middle',
    floating: false,
    fontSize: '14px',
    offsetX: 0,
    offsetY: -10
  },
  dataLabels: {
    enabled: false
  },
  responsive: [{
    breakpoint: 600,
    options: {
      chart: {
        height: 240
      },
      legend: {
        show: false
      },
    }
  }]
};

const carsByBrand: ChartType = {
  chart: {
    height: 320,
    type: 'donut',
  },
  series: [44, 55, 41, 17, 15],
  legend: {
    show: true,
    position: 'bottom',
    horizontalAlign: 'center',
    verticalAlign: 'middle',
    floating: false,
    fontSize: '14px',
    offsetX: 0,
    offsetY: -10
  },
  dataLabels: {
    enabled: false
  },
  labels: ['Chevrolet', 'Dodge', 'Fiat', 'Ford', 'VolksWagen'],
  colors: ['#3bafda', '#26c6da', '#80deea', '#00b19d', '#d1dee4'],
  responsive: [{
    breakpoint: 600,
    options: {
      chart: {
        height: 240
      },
      legend: {
        show: false
      },
    }
  }],
  fill: {
    type: 'gradient'
  }
};


const productByState: ChartType = {
  chart: {
    height: 380,
    type: 'bar',
    toolbar: {
      show: false
    }
  },
  plotOptions: {
    bar: {
      horizontal: true,
    }
  },
  dataLabels: {
    enabled: false
  },
  series: [{
    data: [100, 130, 80, 210, 70, 95]
  }],
  colors: ['#1abc9c'],
  xaxis: {
    // tslint:disable-next-line: max-line-length
    categories: ['Minas Gerais', 'São Paulo', 'Rio de Janeiro', 'Goias', 'Curitiba', 'Bahia'],
  },
  states: {
    hover: {
      filter: 'none'
    }
  },
  grid: {
    borderColor: '#f1f3fa'
  }
};

const usersStatus: ChartType = {
  chart: {
    height: 380,
    type: 'bar',
    toolbar: {
      show: false
    }
  },
  plotOptions: {
    bar: {
      horizontal: true,
    }
  },
  dataLabels: {
    enabled: false
  },
  series: [{
    data: [230, 38]
  }],
  colors: ['#3BAFDA'],
  xaxis: {
    // tslint:disable-next-line: max-line-length
    categories: ['Ativos', 'Inativos'],
  },
  states: {
    hover: {
      filter: 'none'
    }
  },
  grid: {
    borderColor: '#3BAFDA'
  }
};


export {
  productStatusChart, alertReadingStatusChart, carsByBrand, productByState, usersStatus
};
