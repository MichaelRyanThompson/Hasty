import React from 'react';
import { Container, Row, Col } from 'react-bootstrap';
import Header from '../../../components/elements/Header';
import Rewards from '../../../components/rewards/Rewards';

function RewardsDashboard(props) {
  const crumbs = [
    { name: 'Dashboard', path: '/dashboard' },
    { name: 'Rewards', path: '/dashboard/rewards' },
  ];

  return (
    <>
      <Container>
        <Row>
          <Col>
            <Header title="Rewards Dashboard" crumbs={crumbs} />
          </Col>
        </Row>
        <Row>
          <Col>
            <Rewards {...props} />
          </Col>
        </Row>
      </Container>
    </>
  );
}

export default RewardsDashboard;
