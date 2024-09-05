import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

function App() {
  return (
    <div className="App vh-100 d-flex">
      <Container className='p-5 bg-custom'>
        <Row className="text-white text-center p-2">
          <h1>Maple Client</h1>
        </Row>
        <Row>
          <Col className="text-white text-center p-2">
            <div className='form-group d-inline-flex align-items-center'>
              <span className='mx-2'>IP Address</span>
              <input className='form-control' style={{ width: '150px' }} />
              <span className='mx-2'>Port</span>
              <input className='form-control' style={{ width: '80px' }} />
            </div>
          </Col>
        </Row>
        <Row>
          <Col className="text-white text-center p-2">
            <div className='form-group d-inline-flex align-items-center'>
              <span className='mx-2'>Send text:</span>
              <input className='form-control' style={{ width: '150px' }} />
              <button className='btn btn-primary mx-2'>Send</button>
            </div>
          </Col>
        </Row>
        <Row>
          <Col className="text-white text-center p-2">
            <div className='form-group d-inline-flex align-items-center'>
              <h4>Get sensor readings</h4>
              <button className='btn btn-primary mx-2'>Update</button>
            </div>
          </Col>
        </Row>        
        <Row>
          <Col>
            <div className='form-group d-inline-flex p-2 align-items-center'>
              <p className='font-weight-bold text-white'>Built by @jorgedevs</p>
            </div>
          </Col>
        </Row>
      </Container>
    </div>
  );
}

export default App;
