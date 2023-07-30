import {Col, Row} from "reactstrap";
import EChartsReact from "echarts-for-react";
import {useEffect, useState} from "react";
import {Loading} from "./Loading";
import Stat from "./Stat";

export default function Grid(props) {
  let [stats, setStats] = useState()
  let [chart, setChart] = useState()

  useEffect(() => {
    fetch("/api/Stats")
      .then(r => r.json())
      .then(j => setStats(j));
  }, []);

  useEffect(() => {
    fetch("/api/Chart")
      .then(r => r.json())
      .then(j => setChart(j));
  }, []);



  if (props.item && chart && stats) {
    return (
      <>
        <Row>
          <Col sm="9">
            <div>
              <img alt="Item" src={props.item.Image} />
            </div>
            <div>
              <h2>{props.item.Name}</h2>
            </div>
          </Col>
          <Col sm="3">
            <Row>
              <Col>
                <div>Now</div>
                <Stat value={stats.Day} />
              </Col>
            </Row>
            <Row>
              <Col>
                <div>Yesterday</div>
                <Stat value={stats.Month} />
              </Col>
            </Row>
            <Row>
              <Col>
                <div>Last month</div>
                <Stat value={stats.Year} />
              </Col>
            </Row>
          </Col>
        </Row>
        <Row>
          <Col>
            <EChartsReact option={chart} />
          </Col>
        </Row>
      </>
    )
  } else {
    return <Loading />
  }
}