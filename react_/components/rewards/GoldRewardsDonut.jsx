import React from 'react';
import { Col } from 'react-bootstrap';
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
import { Doughnut } from 'react-chartjs-2';
import PropTypes from 'prop-types';

ChartJS.register(ArcElement, Tooltip, Legend);

function GoldRewardsDonut(props) {
  // chart data
  const donutChartData = {
    labels: ['Maximum Points'],
    datasets: [
      {
        data: [props.runningTotals.totalLifetimePoints],
        backgroundColor: ['#CB9622'],
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
      const { ctx } = chart;
      const xCoor = chart.getDatasetMeta(0).data[0].x;
      const yCoor = chart.getDatasetMeta(0).data[0].y;
      ctx.save();
      ctx.font = 'bolder 22px sans-serif';
      ctx.fillStyle = '#CB9622';
      ctx.fillText(`You are`, xCoor, yCoor - 20);
      ctx.font = 'bolder 35px sans-serif';
      ctx.fillStyle = '#CB9622';
      ctx.textAlign = 'center';
      ctx.textBaseline = 'middle';
      ctx.fillText(`Golden`, xCoor, yCoor + 10);
    },
  };

  return (
    <Col>
      <h4 className="header-title bolder text-center mb-3">Congratulations</h4>
      <div className="mb-5 mt-4 chartjs-chart rewardsdonutchart-height-width">
        <Doughnut data={donutChartData} options={donutChartOpts} plugins={[textCenter]} />
      </div>
    </Col>
  );
}

GoldRewardsDonut.propTypes = {
  runningTotals: PropTypes.shape({
    totalAvailablePoints: PropTypes.number.isRequired,
    totalLifetimePoints: PropTypes.number.isRequired,
    totalPointsRedeemed: PropTypes.number.isRequired,
  }).isRequired,
};

export default GoldRewardsDonut;
