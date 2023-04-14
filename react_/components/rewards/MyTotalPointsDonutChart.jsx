import React from 'react';
import { Col } from 'react-bootstrap';
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
import { Doughnut } from 'react-chartjs-2';
import PropTypes from 'prop-types';

ChartJS.register(ArcElement, Tooltip, Legend);

function MyTotalPointsDonutChart(props) {
  // chart data
  const donutChartData = {
    labels: ['Points Remaining', 'Points Spent/Expired'],
    datasets: [
      {
        data: [props.runningTotals.totalAvailablePoints, Math.abs(props.runningTotals.totalPointsRedeemed)],
        backgroundColor: ['#5C2563', '#F23439'],
        borderColor: 'transparent',
        borderWidth: '3',
      },
    ],
  };
  // default options
  const donutChartOpts = {
    maintainAspectRatio: false,
    cutout: '60%',
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
      ctx.font = 'bolder 20px sans-serif';
      ctx.fillStyle = '#5C2563';
      ctx.fillText(`${data.datasets[0].data[0]} Remain`, xCoor, yCoor - 15);
      ctx.font = 'bolder 20px sans-serif';
      ctx.fillStyle = '#F23439';
      ctx.textAlign = 'center';
      ctx.textBaseline = 'middle';
      ctx.fillText(`+ ${data.datasets[0].data[1]} Spent`, xCoor, yCoor + 10);
    },
  };

  return (
    <Col>
      <h4 className="header-title bolder text-center mb-3">My Total Points</h4>
      <div className="mb-5 mt-4 chartjs-chart rewardsdonutchart-height-width">
        <Doughnut data={donutChartData} options={donutChartOpts} plugins={[textCenter]} />
      </div>
    </Col>
  );
}

MyTotalPointsDonutChart.propTypes = {
  runningTotals: PropTypes.shape({
    totalAvailablePoints: PropTypes.number.isRequired,
    totalLifetimePoints: PropTypes.number.isRequired,
    totalPointsRedeemed: PropTypes.number.isRequired,
  }).isRequired,
};

export default MyTotalPointsDonutChart;
