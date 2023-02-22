import {Button, ButtonGroup, Col, Row} from "reactstrap";
import {useEffect, useState} from "react";
import Item from "./components/Item";
import Editor from "./components/Editor";
import Chart from "./components/Chart";

export default function App() {
  let [items, setItems] = useState([])
  let [type, setType] = useState("")
  let [chart, setChart] = useState(0)
  let [error, setError] = useState()

  useEffect(() => {
    if (type === "") {
      setItems([])
      fetch("/api/Item/GetItems")
        .then(r => r.json())
        .then(j => j.success ? setItems(j.data) : setError(true))
    }
  }, [type])

  useEffect(() => {

  })

  let showChart = (id) => {
    if (items.length > 0) {
      return <Chart item={items[id]}/>
    } else {
      return <></>
    }
  }

  return (
    <>
      <Row className="m-0 full-height">
        <Col id="chart" className="fill-height">
          {showChart(chart)}
        </Col>
        <Col xs="2" className="pe-0 full-height side-tab">
          <ButtonGroup className="fill-width pb-3" size="lg">
            <Button className="radius-lef-zero" onClick={() => setType("add")}>Add</Button>
            <Button onClick={() => setType("edit")}>Edit</Button>
            <Button className="radius-right-zero" onClick={() => setType("delete")}>Delete</Button>
          </ButtonGroup>
          <div className="fill-height scroll-container">
            {items.map((item, index) => (
              <div key={index} onClick={() => setChart(index)} className="item">
                <Item id={item.key}/>
              </div>
            ))}
          </div>
        </Col>
      </Row>
      <Editor type={type} setType={setType} items={items}/>
    </>
  )
}