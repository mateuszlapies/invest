import {useEffect, useState} from "react";
import {Card, CardBody, CardImg, Col, Row} from "reactstrap";

export default function Item(props) {
  let [item, setItem] = useState();
  let [currencies, setCurrencies] = useState([]);
  let [error, setError] = useState(false);

  useEffect(() => {
    fetch("api/Item/GetItem?id=" + props.id)
      .then(r => r.json())
      .then(j => j.success ? setItem(j.data) : setError(true))
  }, [props.id])

  useEffect(() => {
    fetch("api/Type/Currency")
      .then(r => r.json())
      .then(j => j.success ? setCurrencies(j.data) : setError(true))
  }, [])

  let profit = (buyPrice, sellPrice, buyAmount) => {
    return (sellPrice - buyPrice) * buyAmount;
  }

  let price = (number, currency) => {
    return Number(number).toLocaleString(navigator.language, {style: "currency", currency: currencies[currency]})
  }

  if (item) {
    return (
      <div className="pb-3 ps-3 pe-3">
        <Card className={props.active ? "shadow" : ""}>
          <CardImg className="p-2" alt="Item icon" src={item.url}/>
          <CardBody className="text-center">
            <Row className="m-0">
              <Col>
                <b>{item.name}</b>
              </Col>
            </Row>
            <Row className="m-0">
              <Col>Amount: {item.buyAmount}</Col>
            </Row>
            <Row className="m-0">
              <Col xs="2">Buy:</Col><Col>{price(item.buyPrice, item.currency)}</Col>
              <Col xs="2">Sell:</Col><Col>{price(item.sellPrice, item.currency)}</Col>
            </Row>
            <Row>
              <Col>Profit:</Col><Col>{price(profit(item.buyPrice, item.sellPrice, item.buyAmount), item.currency)}</Col>
            </Row>
          </CardBody>
        </Card>
      </div>
    )
  }
  return <>{error}</>
}