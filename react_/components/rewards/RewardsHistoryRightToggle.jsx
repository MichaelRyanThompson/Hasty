import React, { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Offcanvas from 'react-bootstrap/Offcanvas';
import RewardsHistoryTable from './RewardsHistoryTable';
import PropTypes from 'prop-types';
import './patriotpointsrewards.css';

function RewardsHistoryRightToggle({ ...props }) {
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  return (
    <>
      <Button variant="primary" onClick={handleShow} className="me-2 rounded">
        View Rewards History
      </Button>
      <Offcanvas className="rewardshistory-offcanvas" show={show} onHide={handleClose} {...props}>
        <Offcanvas.Header closeButton>
          <h3 className="offcanvas-title">Rewards History</h3>
        </Offcanvas.Header>
        <Offcanvas.Body>
          <RewardsHistoryTable {...props} />
        </Offcanvas.Body>
      </Offcanvas>
    </>
  );
}

RewardsHistoryRightToggle.propTypes = {
  placement: PropTypes.string.isRequired,
  rewardsData: PropTypes.shape({
    pageIndex: PropTypes.number.isRequired,
    pageSize: PropTypes.number.isRequired,
    rewardsList: PropTypes.arrayOf(
      PropTypes.shape({
        id: PropTypes.number.isRequired,
        sourceId: PropTypes.number.isRequired,
        points: PropTypes.number.isRequired,
        dateCreated: PropTypes.string.isRequired,
        patriotPointsSource: PropTypes.shape({
          id: PropTypes.number.isRequired,
          name: PropTypes.string.isRequired,
          pointsAwarded: PropTypes.number.isRequired,
          description: PropTypes.string.isRequired,
          imageUrl: PropTypes.string,
          isExpired: PropTypes.bool.isRequired,
          isDeleted: PropTypes.bool.isRequired,
          dateCreated: PropTypes.string.isRequired,
          dateModified: PropTypes.string.isRequired,
          createdBy: PropTypes.number.isRequired,
          modifiedBy: PropTypes.number.isRequired,
        }),
      })
    ),

    rewardsTotals: PropTypes.shape({
      totalLifetimePoints: PropTypes.number.isRequired,
      totalPointsRedeemed: PropTypes.number.isRequired,
      totalAvailablePoints: PropTypes.number.isRequired,
    }).isRequired,
    totalCount: PropTypes.number.isRequired,
  }),
};

export default RewardsHistoryRightToggle;
