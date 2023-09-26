import {Col, Row} from "reactstrap";
import {useEffect, useState} from "react";
import QRCode from "react-qr-code";
import {Loading} from "./Loading";

export function Mobile() {
  let [address, setAddress] = useState(undefined)

  useEffect(() => {
    fetch("/api/Setup")
      .then(r => r.text())
      .then(a => setAddress(a));
  }, []);

  function mobile() {
    return <QRCode value="lol" />
  }

  function details(a) {
    if (a) {
      return <QRCode value={a} />
    } else {
      return <Loading/>
    }
  }

  return (
    <div className="d-table fill">
      <div className="d-table-cell center">
        <h2>No item had been added</h2>
        <h5>To do that you need to use the mobile app</h5>
        <h5>You can download it using QR Code below or by searching `name`</h5>
        <Row>
          <Col><h5>Mobile app</h5></Col>
          <Col><h5>Details</h5></Col>
        </Row>
        <Row>
          <Col>
            {mobile()}
          </Col>
          <Col>
            {details(address)}
          </Col>
        </Row>
      </div>
    </div>
  )
}