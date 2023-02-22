import {useEffect, useState} from "react";
import {Card, CardBody, CardImg, Col, Row} from "reactstrap";

export default function Item(props) {
  let [item, setItem] = useState();
  let [error, setError] = useState();

  useEffect(() => {
    fetch("api/Item/GetItem?id=" + props.id)
      .then(r => r.json())
      .then(j => j.success ? setItem(j.data) : setError(true))
  }, [props.id])

  let profit = (buyPrice, sellPrice, buyAmount) => {
    return (sellPrice - buyPrice) * buyAmount;
  }

  if (item) {
    return (
      <div className="pt-3 ps-3 pe-3">
        <Card>
          <CardImg className="p-2" alt="Item icon" src={item.url}/>
          <CardBody className="text-center">
            <Row className="m-0">
              <Col>
                <b>{item.name}</b>
              </Col>
            </Row>
            <Row className="m-0">
              <Col>Buy:</Col><Col>{item.buyPrice}zł</Col>
              <Col>Amount:</Col><Col>{item.buyAmount}</Col>
            </Row>
            <Row className="m-0">
              <Col>Sell:</Col><Col>0zł</Col>
              <Col>Profit:</Col><Col>{profit(item.buyPrice, 0, item.buyAmount)}zł</Col>
            </Row>
          </CardBody>
        </Card>
      </div>
    )
  }
  return <>{error}</>
}