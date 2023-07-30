import React, {useEffect, useState} from 'react';
import Layout from "./components/Layout";
import Grid from "./components/Grid";
import {Mobile} from "./components/Mobile";

import "./styles/App.css";

export default function App() {
  let [item, setItem] = useState(0);
  let [items, setItems] = useState([]);
  
  useEffect(() => {
    fetch("/api/Item")
      .then(r => r.json())
      .then(j => setItems(j))
  }, []);
  
  useEffect(() => {
    if (items) {
      setInterval(() => {
        setItem((item + 1) % items.length)
      }, 3000)
    }
  }, [item, items])

  if (items.length > 0) {
    return (
      <Layout>
        <Grid item={items[item]} />
      </Layout>
    );
  } else {
    return (
      <Mobile/>
    )
  }

}
