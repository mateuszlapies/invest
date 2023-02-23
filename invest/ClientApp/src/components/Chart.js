import {
  Bar,
  CartesianGrid, ComposedChart,
  Line,
  LineChart,
  ReferenceArea,
  ReferenceLine,
  Tooltip,
  XAxis,
  YAxis
} from "recharts";
import {useEffect, useState} from "react";
import {Button, ButtonToolbar} from "reactstrap";

const tempZoom = {
  refAreaLeft: undefined,
  refAreaRight: undefined,
  left: 'dataMin',
  right: 'dataMax',
  top: 'dataMax',
  bottom: 'dataMin',
  top2: "dataMax",
  bottom2: "0"
}

export default function Chart(props) {
  let [item, setItem] = useState();
  let [zoom, setZoom] = useState(tempZoom)
  let [error, setError] = useState(false);

  let [value, setValue] = useState(true);
  let [volume, setVolume] = useState(true);

  useEffect(() => {
    fetch("/api/Item/GetChart?id=" + props.item.key + "&type=" + props.type)
      .then(r => r.json())
      .then(j => {
        if (j.success) {
          j.data.points.forEach(item => {
            item.date = new Date(item.date).getTime()
          })
          setItem(j.data)
          zoomOut()
        } else {
          setError(true)
        }
      })
  }, [props.item.key, props.type])

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
      return <ReferenceLine yAxisId="1" y={price} label={"Buy Price"} stroke="orange" strokeDasharray="3 3" />
    }
  }

  let selection = (left, right) => {
     if (left && right) {
       return <ReferenceArea yAxisId="1" x1={zoom.refAreaLeft} x2={zoom.refAreaRight} strokeOpacity={0.3} />
     }
  }

  let getAxisYDomain = (from, to, ref, offset) => {
    from = item.points.map(i => i.date).indexOf(from);
    to = item.points.map(i => i.date).indexOf(to);
    const refData = item.points.slice(from - 1, to);
    let [bottom, top] = [refData[0][ref], refData[0][ref]];
    refData.forEach((d) => {
      if (d[ref] > top) top = d[ref];
      if (d[ref] < bottom) bottom = d[ref];
    });

    return [(bottom | 0) - offset, (top | 0) + offset];
  };

  let getZoom = () => {
    let { refAreaLeft, refAreaRight } = zoom;

    if (refAreaLeft === refAreaRight || !refAreaRight) {
      setZoom(zoom => ({...zoom, refAreaLeft: undefined, refAreaRight: undefined}));
      return;
    }

    // xAxis domain
    if (refAreaLeft > refAreaRight) [refAreaLeft, refAreaRight] = [refAreaRight, refAreaLeft];

    // yAxis domain
    const [bottom, top] = getAxisYDomain(refAreaLeft, refAreaRight, 'value', 1);
    const [bottom2, top2] = getAxisYDomain(refAreaLeft, refAreaRight, 'volume', 50);

    setZoom({
      refAreaLeft: undefined,
      refAreaRight: undefined,
      left: refAreaLeft,
      right: refAreaRight,
      bottom,
      bottom2,
      top,
      top2
    })
  }

  let zoomOut = () => {
    setZoom(tempZoom);
  }

  let formatter = (value, name) => {
    switch (name) {
      case "value":
        return [Math.round(value * 100) / 100 + "zł", "Price"]
      case "volume":
        return [value, "Volume"]
    }
  }

  if (item) {
    return (
      <div className="position-relative fill-height">
        <ComposedChart data={item.points}
                   onMouseUp={getZoom}
                   onMouseDown={(e) => {
                     if (e) {
                       setZoom(zoom => ({...zoom, refAreaLeft: e.activeLabel}))
                     }
                   }}
                   onMouseMove={(e) => {
                     if (zoom.refAreaLeft)
                       setZoom(zoom => ({...zoom, refAreaRight: e.activeLabel}))
                   }}
                   width={getContentWidth(document.getElementById('chart'))}
                   height={getContentHeight(document.getElementById('chart'))}>
          <CartesianGrid />
          <Line hide={!value} yAxisId="1" type="monotone" dot={false} dataKey="value" stroke="#AAFF00"/>
          <Line hide={!volume} yAxisId="2" type="step" dot={false} dataKey="volume" stroke="#00B5E2"/>
          <XAxis
            stroke="#fff"
            type="number"
            allowDataOverflow
            dataKey="date"
            domain={[zoom.left, zoom.right]}
            tickFormatter={(value) => new Date(value).toLocaleDateString()}
          />
          <YAxis stroke="#fff" yAxisId="1" allowDataOverflow domain={[zoom.bottom, zoom.top]} />
          <YAxis stroke="#fff" yAxisId="2" allowDataOverflow domain={[zoom.bottom2, zoom.top2]} orientation="right" />
          <Tooltip
            formatter={formatter}
            labelFormatter={(label) => [new Date(label).toLocaleDateString()]}
          />
          {buyPrice(item.buyPrice)}
          {selection(zoom.refAreaLeft, zoom.refAreaRight)}
        </ComposedChart>
        <div className="position-absolute on-top zoom-out">
          <div>
            <Button size="sm" onClick={zoomOut}><i className="bi bi-zoom-out"/></Button>
          </div>
          <div className="mt-2">
            <Button size="sm" onClick={() => setValue(!value)}><i className="bi bi-graph-up-arrow"/></Button>
          </div>
          <div className="mt-2">
            <Button size="sm" onClick={() => setVolume(!volume)}><i className="bi bi-bar-chart-line-fill"/></Button>
          </div>
        </div>
      </div>
    )
  } else {
    return <>{error}</>
  }
}