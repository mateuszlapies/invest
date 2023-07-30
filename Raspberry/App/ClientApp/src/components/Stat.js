import "../styles/Stat.css";

export default function Stat(props) {
  function direction(value) {
    if (value > 0) {
      return <i className="bi bi-arrow-up-right me-3"/>;
    } else if (value < 0) {
      return <i className="bi bi-arrow-down-right me-3" />;
    }

    return <i className="bi bi-arrow-right me-3"/>;
  }

  function color(value) {
    if (value > 0) {
      return "up";
    } else if (value < 0) {
      return "down";
    }

    return "middle";
  }

  return (
    <div className={color(props.value)}>{direction(props.value)}{props.value}</div>
  )
}