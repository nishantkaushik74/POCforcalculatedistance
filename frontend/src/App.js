import logo from './logo.svg';
import './App.css';
import { useState } from 'react';

function App() {
  const [from, setFrom]=useState('');
  const [to, setTo]=useState('');
  const [data, setData]=useState('');
  const findDistance= async () => {
    debugger;
    const response = await fetch(`https://localhost:32768/api/Location/getDistance/${from}/${to}`);
    const json = await response.json();
    setData(json.message);

  };
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <input type="text" onChange={e=>{setFrom(e.currentTarget.value)}} placeholder='from zip code'/>
        <input type="text" onChange={e=>{setTo(e.currentTarget.value)}} placeholder='to zip code'/>
       <button onClick={findDistance}>Find Distance</button>
       {data!='' && <h2>{data}</h2>}
      </header>
    </div>
  );
}

export default App;
