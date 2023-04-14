import React from 'react';
import { Col } from 'react-bootstrap';
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
import { Doughnut } from 'react-chartjs-2';
import PropTypes from 'prop-types';

ChartJS.register(ArcElement, Tooltip, Legend);

function MaxLifeTimeDonutChart(props) {
  // chart data
  const donutChartData = {
    labels: ['Maximum Points'],
    datasets: [
      {
        data: [props.runningTotals.totalLifetimePoints],
        backgroundColor: ['#9ACE26'],
        borderColor: 'transparent',
        borderWidth: '3',
      },
    ],
  };

  // default options
  const donutChartOpts = {
    maintainAspectRatio: false,
    cutout: '60%',
    rotation: 90 * Math.PI,
    plugins: {
      legend: {
        display: false,
      },
    },
  };

  const textCenter = {
    id: 'textCenter',
    afterDatasetsDraw(chart) {
      const { ctx, data } = chart;
      const xCoor = chart.getDatasetMeta(0).data[0].x;
      const yCoor = chart.getDatasetMeta(0).data[0].y;
      ctx.save();
      ctx.font = 'bolder 40px sans-serif';
      ctx.fillStyle = '#9ACE26';
      ctx.textAlign = 'center';
      ctx.textBaseline = 'middle';
      ctx.fillText(`${data.datasets[0].data[0]}`, xCoor, yCoor);
    },
  };

  return (
    <Col>
      <h4 className="header-title bolder text-center mb-3">Lifetime Total</h4>
      <div className="mb-5 mt-4 chartjs-chart rewardsdonutchart-height-width">
        <Doughnut data={donutChartData} options={donutChartOpts} plugins={[textCenter]} />
      </div>
    </Col>
  );
}

MaxLifeTimeDonutChart.propTypes = {
  runningTotals: PropTypes.shape({
    totalAvailablePoints: PropTypes.number.isRequired,
    totalLifetimePoints: PropTypes.number.isRequired,
    totalPointsRedeemed: PropTypes.number.isRequired,
  }).isRequired,
};

export default MaxLifeTimeDonutChart;
