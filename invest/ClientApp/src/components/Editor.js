import {
  Button,
  ButtonDropdown, DropdownItem, DropdownMenu, DropdownToggle,
  Form,
  FormGroup,
  Input,
  InputGroup,
  Label,
  Offcanvas,
  OffcanvasBody,
  OffcanvasHeader
} from "reactstrap";
import {useEffect, useState} from "react";

let tempItem = {
  "name": "",
  "hash": "",
  "buyPrice": 0,
  "currency": 0,
  "buyAmount": 0
}

export default function Editor(props) {
  let [item, setItem] = useState(tempItem)
  let [selected, setSelected] = useState(1)
  let [error, setError] = useState(false)
  let [currency, setCurrency] = useState(false)
  let [currencies, setCurrencies] = useState([])

  useEffect(() => {
    if (props.type === "edit" || props.type === "delete") {
      if (props.items.length > 0) {
        fetch("/api/Item/GetItem?id=" + selected)
          .then(r => r.json())
          .then(j => j.success ? setItem(j.data) : setError(true))
      }
    } else {
      setItem(tempItem)
    }
  }, [props.type, props.items.length, selected])

  useEffect(() => {
    fetch("/api/Type/Currency")
      .then(r => r.json())
      .then(j => j.success ? setCurrencies(j.data) : setError(true))
  }, [])

  let title = (type) => {
    switch (type) {
      case "add":
        return "Add a new item";

      case "edit":
        return "Edit an item";

      case "delete":
        return "Delete an item";

      default:
        return "";
    }
  }

  let select = (type) => {
    switch (type) {
      default:
      case "add":
        return <></>

      case "edit":
      case "delete":
        return (
          <FormGroup>
            <Label for="select">
              Select
            </Label>
            <Input
              id="select"
              name="select"
              type="select"
              value={selected}
              onChange={(e) => setSelected(Number.parseInt(e.target.value))}
            >
              {props.items.map((item, index) => (
                <option value={item.key} key={index}>{item.value}</option>
              ))}
            </Input>
          </FormGroup>
        )
    }
  }

  let color = (type) => {
    switch (type) {
      case "add":
        return "success";

      case "edit":
        return "warning";

      case "delete":
        return "danger";

      default:
        return "primary";
    }
  }

  let disabled = (type) => {
    switch (type) {
      case "add":
      case "edit":
        return false;

      default:
      case "delete":
        return true;
    }
  }

  let onChange = (e) => {
    let {name, value} = e.target;
    setItem(itemState => ({...itemState, [name]: value}))
  }

  let onSubmit = (e) => {
    e.preventDefault()
    switch (props.type) {
      case "add": {
        fetch("/api/Item/CreateItem", {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(item)
        })
          .then(r => r.json())
          .then(j => j.success ? props.setType("") : setError(true))
        break;
      }

      case "edit": {
        fetch("/api/Item/UpdateItem", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(item)
        })
          .then(r => r.json())
          .then(j => j.success ? props.setType("") : setError(true))
        break;
      }

      case "delete": {
        fetch("/api/Item/DeleteItem?id=" + item.itemId, {
          method: "DELETE"
        })
          .then(r => r.json())
          .then(j => j.success ? props.setType("") : setError(true))
        break;
      }

      default: {
        break;
      }
    }
  }

  return (
    <>
      <Offcanvas
        direction="end"
        toggle={() => props.setType("")}
        isOpen={props.type !== ""}
      >
        <OffcanvasHeader toggle={() => props.setType("")}>
          {title(props.type)}
        </OffcanvasHeader>
        <OffcanvasBody>
          <Form onSubmit={onSubmit}>
            {select(props.type)}
            <FormGroup>
              <Label for="name">
                Name
              </Label>
              <Input  id="name" name="name" type="text" value={item.name} disabled={disabled(props.type)} onChange={onChange}/>
            </FormGroup>
            <FormGroup>
              <Label for="hash">
                Hash
              </Label>
              <Input id="hash" name="hash" type="text" value={item.hash} disabled={disabled(props.type)} onChange={onChange}/>
            </FormGroup>
            <FormGroup>
              <Label for="buy">
                Buy Price
              </Label>
              <InputGroup>
                <ButtonDropdown isOpen={currency} toggle={() => setCurrency(!currency)}>
                  <DropdownToggle caret>
                    {currencies[item.currency]}
                  </DropdownToggle>
                  <DropdownMenu className="currency">
                    {currencies.map((item, index) => (
                      <DropdownItem key={index} disabled={index === item.currency} onClick={() => onChange({target: {name: "currency", value: index}})}>
                        {item}
                      </DropdownItem>
                    ))}
                  </DropdownMenu>
                </ButtonDropdown>
                <Input id="buy" name="buyPrice" type="number" value={item.buyPrice} disabled={disabled(props.type)} onChange={onChange}/>
              </InputGroup>
            </FormGroup>
            <FormGroup>
              <Label for="amount">
                Amount
              </Label>
              <Input id="amount" name="buyAmount" type="number" value={item.buyAmount} disabled={disabled(props.type)} onChange={onChange}/>
            </FormGroup>
            <Button color={color(props.type)} type="submit" className="mt-4 fill-width">
              Confirm
            </Button>
          </Form>
        </OffcanvasBody>
      </Offcanvas>
    </>
  )
}