import React, { useEffect, useState } from 'react';
import { Row, Card, Col, Container } from 'react-bootstrap';
import patriotPointsService from '../../services/patriotPointsService';
import PropTypes from 'prop-types';
import toastr from 'toastr';
import MyTotalPointsDonutChart from './MyTotalPointsDonutChart';
import LifetimeTotalDonutChart from './LifetimeTotalDonutChart';
import ToSilverRewardsDonut from './ToSilverRewardsDonut';
import ToGoldRewardsDonut from './ToGoldRewardsDonut';
import ToBronzeRewardsDonut from './ToBronzeRewardsDonut';
import GoldRewardsDonut from './GoldRewardsDonut';
import MaxLifeTimeDonutChart from './MaxLifeTimeDonutChart';
import RewardsHistoryRightToggle from './RewardsHistoryRightToggle';
import Swal from 'sweetalert2';
import './patriotpointsrewards.css';

function Rewards(props) {
  const [pageData, setPageData] = useState({
    rewardsTotals: [],
    rewardsList: [],
    pageIndex: 1,
    pageSize: 10,
    totalCount: 0,
  });

  useEffect(() => {
    patriotPointsService.getByUserIdTotals(props.currentUser.id).then(onGetTotalsSuccess).catch(onGetTotalsError);
  }, []);

  useEffect(() => {
    rewardsPaginated();
  }, [pageData.pageIndex]);

  const rewardsPaginated = () => {
    patriotPointsService
      .getByUserIdPaginated(pageData.pageIndex - 1, pageData.pageSize, props.currentUser.id)
      .then(onGetListSuccess)
      .catch(onGetListError);
  };
  const onGetTotalsSuccess = (response) => {
    const points = response.item;
    setPageData((prevState) => {
      const newData = { ...prevState };
      newData.rewardsTotals = points;
      return newData;
    });
  };

  const onGetTotalsError = () => {
    toastr['error']('Unable to retrieve reward points data');
  };

  const onGetListSuccess = (response) => {
    const pagedResponse = response?.item?.pagedItems;
    const totalCount = response?.item?.totalCount;
    setPageData((prevState) => {
      const newData = { ...prevState };
      newData.rewardsList = pagedResponse;
      newData.totalCount = totalCount;
      return newData;
    });
  };

  const onPageChange = (page) => {
    setPageData((prevState) => {
      const newData = { ...prevState };
      newData.pageIndex = page;
      return newData;
    });
  };

  const onGetListError = () => {
    toastr['error']('Unable to retrieve reward list data');
  };

  function displayCurrentMemberRewardTier() {
    if (pageData.rewardsTotals.totalLifetimePoints < 250) {
      return null;
    } else if (pageData.rewardsTotals.totalLifetimePoints < 500) {
      return (
        <img
          height={75}
          src="https://sabio-training.s3-us-west-2.amazonaws.com/fb9b4fe1-4a61-4cbe-a7eb-05a18964b7d2/michael-thompson-bronze-badge.png"
          alt="Bronze Badge"
        />
      );
    } else if (pageData.rewardsTotals.totalLifetimePoints < 1000) {
      return (
        <img
          height={75}
          src="https://sabio-training.s3-us-west-2.amazonaws.com/4d59ed42-4572-4eb6-958c-59bb7c65b935/michael-thompson-silver-badge.png"
          alt="Silver Badge"
        />
      );
    } else if (pageData.rewardsTotals.totalLifetimePoints >= 1000) {
      return (
        <img
          height={75}
          src="https://sabio-training.s3-us-west-2.amazonaws.com/0b5352f7-d5d9-49e0-962a-03aa99b4c309/michael-thompson-gold-badge.png"
          alt="Silver Badge"
        />
      );
    }
  }

  function displayDonutChartRewardsLevel() {
    if (pageData.rewardsTotals.totalLifetimePoints < 250) {
      return <ToBronzeRewardsDonut runningTotals={pageData.rewardsTotals} />;
    } else if (pageData.rewardsTotals.totalLifetimePoints < 500) {
      return <ToSilverRewardsDonut runningTotals={pageData.rewardsTotals} />;
    } else if (pageData.rewardsTotals.totalLifetimePoints < 1000) {
      return <ToGoldRewardsDonut runningTotals={pageData.rewardsTotals} />;
    } else if (pageData.rewardsTotals.totalLifetimePoints >= 1000) {
      return <GoldRewardsDonut runningTotals={pageData.rewardsTotals} />;
    }
  }

  function displayLifeTimeTotalDonutCharts() {
    if (pageData.rewardsTotals.totalLifetimePoints >= 1000) {
      return <MaxLifeTimeDonutChart runningTotals={pageData.rewardsTotals} />;
    } else {
      return <LifetimeTotalDonutChart runningTotals={pageData.rewardsTotals} />;
    }
  }

  function redeemReward(e) {
    let userId = props.currentUser.id;
    let sourceId = e.target.value;
    let values = {
      userId,
      sourceId,
    };

    patriotPointsService.add(values).then(redeemRewardSuccess).catch(redeemRewardError);
  }

  function redeemRewardSuccess() {
    Swal.fire({
      icon: 'success',
      title: 'Congrats!',
      text: 'Reward Redeemed! Be Sure To Check Your Email',
      showConfirmButton: false,
      timer: 1500,
    });
    patriotPointsService.getByUserIdTotals(props.currentUser.id).then(onGetTotalsSuccess).catch(onGetTotalsError);
  }

  function redeemRewardError() {
    Swal.fire('Uh-oh..', 'There was an error adding this redemption request.', 'error');
  }

  return (
    <>
      {pageData.rewardsTotals.totalLifetimePoints ? (
        <Row>
          <Col className="mb-2">
            <RewardsHistoryRightToggle placement={'end'} rewardsData={pageData} handlePageChange={onPageChange} />
          </Col>
        </Row>
      ) : null}
      <Card>
        <Row className="tilebox-one px-3 py-2">
          <Col>
            <h3 className="mt-2 text-secondary text-center">My Points</h3>
            <h1 className="text-bold text-secondary text-center patriotpoints-points-text">
              {pageData.rewardsTotals.totalAvailablePoints ? pageData.rewardsTotals.totalAvailablePoints : 0}
            </h1>
          </Col>
          <Col>
            <h3 className="mt-2 text-secondary text-center">Spent</h3>
            <h1 className="text-bold text-secondary text-center patriotpoints-points-text">
              {pageData.rewardsTotals.totalPointsRedeemed ? pageData.rewardsTotals.totalPointsRedeemed : 0}
            </h1>
          </Col>
          <Col>
            <h3 className="mt-2 text-secondary text-center ">Lifetime</h3>
            <h1 className="text-bold text-secondary text-center patriotpoints-points-text">
              {pageData.rewardsTotals.totalLifetimePoints ? pageData.rewardsTotals.totalLifetimePoints : 0}
            </h1>
          </Col>
          {pageData.rewardsTotals.totalLifetimePoints ? (
            <Col>
              <h5 className="mt-2 text-dark bolder text-center">Current Reward Level:</h5>
              <h1 className="text-bold text-secondary text-center">{displayCurrentMemberRewardTier()}</h1>
            </Col>
          ) : null}
        </Row>
      </Card>
      {pageData.rewardsTotals.totalLifetimePoints ? (
        <Row>
          <MyTotalPointsDonutChart runningTotals={pageData.rewardsTotals} />
          {displayDonutChartRewardsLevel()}
          {displayLifeTimeTotalDonutCharts()}
        </Row>
      ) : null}
      <Card className="bg-secondary">
        <Row className="tilebox-one">
          <Col>
            <h3 className="mt-2 text-center text-light">
              Now let&apos;s have some fun! Use your remaining points and choose some great offers below...
            </h3>
          </Col>
        </Row>
      </Card>
      <Container>
        <Row>
          <Col className="col-xs col-sm-6 col-lg-4 py-3">
            <div className="card rounded shadow-lg h-100">
              <img
                src="https://sabio-training.s3-us-west-2.amazonaws.com/9936287d-583d-4420-8ef0-e53d18adc181/michael-thompson-t-shirt-rewards.png"
                className="card-img-top"
                alt="Hasty T-shirt"
              />
              <div className="card-body d-flex flex-column">
                <h5 className="card-title">Hasty T-Shirt or Hoodie</h5>
                <p className="card-text">
                  Get a free t-shirt or hoodie on us. Upon redemption we will send an email asking your size and to
                  confirm where to ship the shirt or hoodie of your choice.
                </p>
                {pageData.rewardsTotals.totalAvailablePoints >= 100 ? (
                  <button value={9} className="btn btn-primary rounded h-25 w-100 mt-auto" onClick={redeemReward}>
                    Redeem
                  </button>
                ) : (
                  <button className="btn patriotpoints-inactive-button rounded h-25 w-100 mt-auto">Redeem</button>
                )}
              </div>
            </div>
          </Col>
          <Col className="col-xs col-sm-6 col-lg-4 py-3">
            <div className="card rounded shadow-lg h-100">
              <img
                src="https://sabio-training.s3-us-west-2.amazonaws.com/841f3b85-68d6-4c34-bb99-c1f0f451d109/michael-thompson-local-restaurant-reward.png"
                className="card-img-top"
                alt="Local Restaurant Gift Card"
              />
              <div className="card-body d-flex flex-column">
                <h5 className="card-title">$50 Gift Card To Any Local Restaurant</h5>
                <p className="card-text">
                  We know moving is tough so enjoy a free meal on us. Upon redemption let us know a restaurant you would
                  like to go to in your new home and we will sent you a gift card.
                </p>
                {pageData.rewardsTotals.totalAvailablePoints >= 150 ? (
                  <button value={10} className="btn btn-primary rounded h-25 w-100 mt-auto" onClick={redeemReward}>
                    Redeem
                  </button>
                ) : (
                  <button className="btn patriotpoints-inactive-button rounded h-25 w-100 mt-auto">Redeem</button>
                )}
              </div>
            </div>
          </Col>
          <Col className="col-xs col-sm-6 col-lg-4 py-3">
            <div className="card rounded shadow-lg h-100">
              <img
                src="https://sabio-training.s3-us-west-2.amazonaws.com/c457a01f-5db0-499e-a375-b20012f2d6fa/michael-thompson-house-cleaning-reward.png"
                className="card-img-top"
                alt="House Cleaning"
              />
              <div className="card-body d-flex flex-column">
                <h5 className="card-title">Free House Cleaning</h5>
                <p className="card-text">
                  If your new place isn&apos;t up to snuff don&apos;t stress we will send someone to help clean it up.
                </p>
                {pageData.rewardsTotals.totalAvailablePoints >= 200 ? (
                  <button value={12} className="btn btn-primary rounded h-25 w-100 mt-auto" onClick={redeemReward}>
                    Redeem
                  </button>
                ) : (
                  <button className="btn patriotpoints-inactive-button rounded h-25 w-100 mt-auto">Redeem</button>
                )}
              </div>
            </div>
          </Col>
          <Col className="col-xs col-sm-6 col-lg-4 py-3">
            <div className="card rounded shadow-lg h-100">
              <img
                src="https://sabio-training.s3-us-west-2.amazonaws.com/d848f128-6fd0-4556-9977-235d8fa9bf44/michael-thompson-home-warming-appliance-reward.png"
                className="card-img-top"
                alt="Kitchen Aid Mixer"
              />
              <div className="card-body d-flex flex-column">
                <h5 className="card-title">House Warming Gift</h5>
                <p className="card-text">Get a kitchen aid mixer sent to your new home on us.</p>
                {pageData.rewardsTotals.totalAvailablePoints >= 250 ? (
                  <button value={13} className="btn btn-primary rounded h-25 w-100 mt-auto" onClick={redeemReward}>
                    Redeem
                  </button>
                ) : (
                  <button className="btn patriotpoints-inactive-button rounded h-25 w-100 mt-auto">Redeem</button>
                )}
              </div>
            </div>
          </Col>
          <Col className="col-xs col-sm-6 col-lg-4 py-3">
            <div className="card rounded shadow-lg h-100">
              <img
                src="https://sabio-training.s3-us-west-2.amazonaws.com/d1361269-e86b-4640-b10e-3ead27121479/michael-thompson-boots-reward.png"
                className="card-img-top"
                alt="Free Pair of Boots"
              />
              <div className="card-body d-flex flex-column">
                <h5 className="card-title">Free Next Pair of Boots</h5>
                <p className="card-text">
                  Get your next pair of boots on us. Upon redemption we will send an email confirming your size and will
                  send you a fresh pair of boots.
                </p>
                {pageData.rewardsTotals.totalAvailablePoints >= 150 ? (
                  <button value={14} className="btn btn-primary rounded h-25 w-100 mt-auto" onClick={redeemReward}>
                    Redeem
                  </button>
                ) : (
                  <button className="btn patriotpoints-inactive-button rounded h-25 w-100 mt-auto">Redeem</button>
                )}
              </div>
            </div>
          </Col>
        </Row>
      </Container>
      <Row>
        <br />
      </Row>
    </>
  );
}

Rewards.propTypes = {
  currentUser: PropTypes.shape({
    id: PropTypes.number.isRequired,
    firstName: PropTypes.string.isRequired,
    lastName: PropTypes.string.isRequired,
    email: PropTypes.string.isRequired,
  }).isRequired,
};

export default Rewards;
