import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  return (
    <div className="App vh-100 d-flex">
      <div className='p-5 bg-custom text-white'>
          <div className='form-group d-inline align-items-center'>
            <h1>Maple Client</h1>
          </div>

          <div className='form-group d-flex align-items-center p-2'>
            <span className='mx-2'>IP Address</span>
            <input className='form-control' style={{ width: '150px' }} />
            <span className='mx-2'>Port</span>
            <input className='form-control' style={{ width: '80px' }} />
          </div>

          <div className='form-group d-flex align-items-center p-2'>
            <span className='mx-2'>Send text:</span>
            <input className='form-control' style={{ width: '150px' }} />
            <button className='btn btn-primary mx-2'>Send</button>
          </div>

          <div className='form-group d-flex align-items-center p-2'>
            <span className='mx-2'>Sensor readings</span>
            <button className='btn btn-primary mx-2'>Update</button>
          </div>

          <div className='form-group d-flex align-items-center p-2'>
            <span className='mx-2'>Rotate servo</span>
            <input type="range" min="0" max="180" value="90" className="form-range" id="myRange" />
          </div>

          <div className='form-group d-flex align-items-center p-2'>
            <p className='font-weight-bold'>Built by @jorgedevs</p>
          </div>

      </div>
    </div>
  );
}

export default App;
