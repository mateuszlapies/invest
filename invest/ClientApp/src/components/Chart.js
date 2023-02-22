import {CartesianGrid, Line, LineChart, ReferenceLine, Tooltip, XAxis, YAxis} from "recharts";
import {useEffect, useState} from "react";

export default function Chart(props) {
  let [item, setItem] = useState();
  let [error, setError] = useState(false);

  useEffect(() => {
    fetch("/api/Item/GetItem?id=" + props.item.key)
      .then(r => r.json())
      .then(j => {
        if (j.success) {
          let data = j.data
          data.points.forEach(item => {
            item.date = new Date(item.date).toLocaleDateString()
          })
          setItem(j.data)
        } else {
          setError(true)
        }
      })
  }, [props.item.key])

  let getContentWidth = (element) => {
    let styles = getComputedStyle(element)
    return element.clientWidth
      - parseFloat(styles.paddingLeft)
      - parseFloat(styles.paddingRight)
  }

  let getContentHeight = (element) => {
    let styles = getComputedStyle(element)
    return element.clientHeight
      - parseFloat(styles.paddingTop)
      - parseFloat(styles.paddingBottom)
  }

  let buyPrice = (price) => {
    if (price > 0) {
      return <ReferenceLine y={price} label={"Buy Price"} stroke="red" strokeDasharray="3 3" />
    }
  }

  if (item) {
    return (
      <LineChart data={item.points}
                 width={getContentWidth(document.getElementById('chart'))}
                 height={getContentHeight(document.getElementById('chart'))}>
        <Line type="monotone" dot={false} dataKey="value"/>
        <CartesianGrid />
        <XAxis dataKey="date"/>
        <YAxis/>
        <Tooltip formatter={(value, name, props) => [Math.round(value * 100) / 100 + "zł", "Price"]}/>
        {buyPrice(item.buyPrice)}
      </LineChart>
    )
  } else {
    return <>{error}</>
  }
}