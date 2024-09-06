import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

function App() {
  return (
    <div className="App vh-100 d-flex align-items-center" >
      <Container className='p-4 bg-custom text-white w-75'>
        <Row className="text-center p-2 my-3">
          <h1>Maple Client</h1>
        </Row>
        <Row className='p-3 bg-red'>
          <Col className="col-2 d-flex align-items-center">
            <span>Enter IP Address:</span>
          </Col>
          <Col className="col-4">
            <input placeholder='192.168.1.1' className='form-control' />
          </Col>
          <Col className="col-2 d-flex align-items-center">
            <span>Enter Port Number:</span>
          </Col>
          <Col className="col-4">
            <input placeholder='3000' className='form-control w-50' />
          </Col>
        </Row>
        <Row className='p-3 bg-blue'>
          <Col className="col-2 d-flex align-items-center">
            <span>Send text:</span>
          </Col>
          <Col className="col-6">
            <input placeholder='Hello world' className='form-control' />
          </Col>
          <Col className="d-flex align-items-center">
            <button className='btn btn-primary'>Send</button>
          </Col>
        </Row>
        <Row className='p-3 bg-red'>
          <Col className="col-2 d-flex align-items-center">
            <span>Sensors:</span>
          </Col>
          <Col className="col-6 d-flex align-items-center justify-content-center">
            <span className='mx-2'>Temperature: 25°C</span>
            <span className='mx-3'>|</span>
            <span className='mx-2'>Pressure: 1.1ATM</span>
            <span className='mx-3'>|</span>
            <span className='mx-2'>Humidity: 88%</span>
          </Col>
          <Col className="d-flex align-items-center">
            <button className='btn btn-primary'>Get readings</button>
          </Col>
        </Row>
        <Row className='p-3 bg-blue'>
          <Col className="col-2 d-flex align-items-center">
            <span>Rotate Servo:</span>
          </Col>
          <Col className="d-flex align-items-center">
            <input type="range" className='w-100' />
          </Col>
          <Col className="col-4 d-flex align-items-center">
            <button className='btn btn-primary'>Rotate</button>
          </Col>
        </Row>      
        <Row className='p-3 my-3'>
          <Col className="d-flex align-items-center justify-content-center">
            <span className='fw-bold'>Built by @jorgedevs</span>
          </Col>
        </Row>
      </Container>
    </div>
  );
}

export default App;
