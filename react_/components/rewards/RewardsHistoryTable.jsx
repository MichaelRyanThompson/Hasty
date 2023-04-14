// @flow
import React from 'react';
import { Row, Col, Card, Table } from 'react-bootstrap';
import Pagination from 'rc-pagination';
import PropTypes from 'prop-types';
import locale from 'rc-pagination/lib/locale/en_US';
import './patriotpointsrewards.css';
import { formatDateTime } from '../../utils/dateFormater';

function RewardsHistoryTable({ ...props }) {
  return (
    <>
      <Card>
        <Card.Body>
          <Table className="mb-0" responsive>
            <thead className="rewardshistory-table-header">
              <tr>
                <th>Name</th>
                <th>Points</th>
                <th>Description</th>
                <th>Date</th>
              </tr>
            </thead>
            <tbody>
              {props.rewardsData.rewardsList.map((reward, index) => {
                return (
                  <tr key={index}>
                    <td>{reward.patriotPointsSource.name}</td>
                    <td>{reward.patriotPointsSource.pointsAwarded}</td>
                    <td>{reward.patriotPointsSource.description}</td>
                    <td>{formatDateTime(reward.dateCreated)}</td>
                  </tr>
                );
              })}
            </tbody>
          </Table>
        </Card.Body>
      </Card>
      <Row>
        <Col xl={6}>
          <Pagination
            onChange={props.handlePageChange}
            current={props.rewardsData.pageIndex}
            total={props.rewardsData.totalCount}
            pageSize={props.rewardsData.pageSize}
            locale={locale}
          />
        </Col>
      </Row>
    </>
  );
}

RewardsHistoryTable.propTypes = {
  placement: PropTypes.string.isRequired,
  handlePageChange: PropTypes.func,
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
        }).isRequired,
      }).isRequired
    ),
    rewardsTotals: PropTypes.shape({
      totalLifetimePoints: PropTypes.number.isRequired,
      totalPointsRedeemed: PropTypes.number.isRequired,
      totalAvailablePoints: PropTypes.number.isRequired,
    }).isRequired,
    totalCount: PropTypes.number.isRequired,
  }),
};

export default RewardsHistoryTable;
