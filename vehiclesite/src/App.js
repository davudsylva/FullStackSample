import React from 'react';
import logo from './logo.svg';
import './App.css';
import CarsTable from './components/carsTable.js'
import { Box } from "@chakra-ui/core";

function App() {
  return (
    <div className="App">
      <Box width="100%" height="100%" background="#ff00ff">
        <CarsTable />
      </Box>
    </div>
  );
}

export default App;
