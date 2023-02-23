import {useEffect, useState} from "react";

const slideInLeft = {
  transition: `transform 350ms ease-in-out`,
  transform: "translateX(-101%)",
  display: "none"
};

const slideInRight = {
  transition: `transform 350ms ease-in-out`,
  transform: "translateX(101%)",
  display: "none"
};

const slideOut = {
  transition: `transform 350ms ease-in-out`,
  transform: "translateX(0)",
  display: "block"
};

const slideTransitionLeft = {
  entering: { transform: "translateX(-101%)" },
  entered: { transform: "translateX(0)" },
  exiting: { transform: "translateX(0)" },
  exited: { transform: "translateX(-101%)" }
};

const slideTransitionRight = {
  entering: { transform: "translateX(101%)" },
  entered: { transform: "translateX(0)" },
  exiting: { transform: "translateX(0)" },
  exited: { transform: "translateX(101%)" }
};

const direction = {
  left: {
    slideIn: slideInLeft,
    slideTransition: slideTransitionLeft
  },
  right: {
    slideIn: slideInRight,
    slideTransition: slideTransitionRight
  }
}

export default function Animation(props) {
  let [style, setStyle] = useState()

  useEffect(() => {
    let slide;

    if (props.state === "entering" && props.state === "entered") {
      slide = direction[props.direction].slideIn;
    } else {
      slide = slideOut;
    }

    setStyle({
      ...slide,
      ...direction[props.direction].slideTransition[props.state]
    })
  }, [props.direction, props.state])

  return (
    <div style={style} className="animation">
      {props.children}
    </div>
  )
}