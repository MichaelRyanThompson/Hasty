import React from 'react';
import { Container, Row, Col } from 'react-bootstrap';
import Header from '../../../components/elements/Header';
import AdminRewards from '../../../components/rewards/AdminRewards';

function AdminRewardsDashboard() {
  const crumbs = [
    { name: 'Dashboard', path: '/dashboard' },
    { name: 'Admin Rewards', path: '/dashboard/admin/rewards' },
  ];

  return (
    <>
      <Container>
        <Row>
          <Col>
            <Header title="Admin Rewards Dashboard" crumbs={crumbs} />
          </Col>
        </Row>
        <Row>
          <Col>
            <AdminRewards />
          </Col>
        </Row>
      </Container>
    </>
  );
}

export default AdminRewardsDashboard;
