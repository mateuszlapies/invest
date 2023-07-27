import {useEffect, useState} from "react";
import Chart from "./components/Chart";
import {AuthContext} from "./contexts/AuthContext";
import {Button, ButtonGroup, ButtonToolbar, Col, Input, InputGroup, InputGroupText, Row} from "reactstrap";
import {Transition} from "react-transition-group";
import Animation from "./components/Animation";
import Item from "./components/Item";
import Editor from "./components/Editor";

export default function Home() {
  let [items, setItems] = useState([])
  let [type, setType] = useState("")
  let [chart, setChart] = useState(0)
  let [itemOptions, setItemOptions] = useState(false)
  let [chartOptions, setChartOptions] = useState(false)
  let [chartType, setChartType] = useState("overall")
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
    if (itemOptions) {
      setTimeout(() => setItemOptions(false), 5000)
    }
  }, [itemOptions])

  useEffect(() => {
    if (chartOptions) {
      setTimeout(() => setChartOptions(false), 5000)
    }
  }, [chartOptions])

  let showChart = (id, type) => {
    if (items.length > 0) {
      return <Chart item={items[id]} type={type}/>
    } else {
      return <></>
    }
  }

  return (
    <AuthContext.Consumer>
      {content => (
        <>
          <Row className="m-0 mb-3 overflow-hidden">
            <Col xs="3" className="ps-0">
              <ButtonToolbar>
                <ButtonGroup size="lg">
                  <Button onClick={() => setChartOptions(!chartOptions)} className="radius-right-zero radius-lef-zero on-top"><i className="bi bi-graph-up"/></Button>
                </ButtonGroup>
                <Transition in={chartOptions} timeout={0}>
                  {state => (
                    <Animation state={state} direction="left">
                      <ButtonGroup size="lg">
                        <Button className="radius-lef-zero" disabled={chartType === "overall"} onClick={() => setChartType("overall")}>Overall</Button>
                        <Button disabled={chartType === "year"} onClick={() => setChartType("year")}>Year</Button>
                        <Button disabled={chartType === "month"} onClick={() => setChartType("month")}>Month</Button>
                        <Button disabled={chartType === "week"} onClick={() => setChartType("week")}>Week</Button>
                        <Button className="radius-right-zero-top" disabled={chartType === "day"} onClick={() => setChartType("day")}>Day</Button>
                      </ButtonGroup>
                    </Animation>
                  )}
                </Transition>
              </ButtonToolbar>
            </Col>
            <Col xs="1"/>
            <Col xs="4">
              <InputGroup>
                <InputGroupText>
                  Price Tracker
                </InputGroupText>
                <Input />
                <InputGroupText>
                  <i className="bi bi-search"/>
                </InputGroupText>
              </InputGroup>
            </Col>
            <Col xs="2"/>
            <Col xs="2" className="pe-0">
              <ButtonToolbar className="flex-end">
                <Transition in={itemOptions} timeout={0}>
                  {state => (
                    <Animation state={state} direction="right">
                      <ButtonGroup size="lg">
                        <Button className="radius-lef-zero-top" onClick={() => setType("add")}>Add</Button>
                        <Button onClick={() => setType("edit")}>Edit</Button>
                        <Button className="radius-right-zero" onClick={() => setType("delete")}>Delete</Button>
                      </ButtonGroup>
                    </Animation>
                  )}
                </Transition>
                <ButtonGroup size="lg">
                  <Button onClick={() => setItemOptions(!itemOptions)} className="radius-right-zero radius-lef-zero on-top"><i className="bi bi-gear-wide-connected"/></Button>
                </ButtonGroup>
              </ButtonToolbar>
            </Col>
          </Row>
          <Row className="m-0 fill-height">
            <Col xs="10" id="chart" className="fill-height">
              {showChart(chart, chartType)}
            </Col>
            <Col xs="2" className="pe-0 side-tab">
              <div className="fill-height scroll-container">
                {items.map((item, index) => (
                  <div key={index} onClick={() => setChart(index)} className="item">
                    <Item id={item.key} active={chart === index}/>
                  </div>
                ))}
              </div>
            </Col>
          </Row>
          <Editor type={type} setType={setType} items={items}/>
        </>
      )}
    </AuthContext.Consumer>
  )
}