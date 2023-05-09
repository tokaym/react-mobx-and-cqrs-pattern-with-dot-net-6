import { Flip, Slide, toast } from "react-toastify";

const WarningToast = (description: string | undefined) => {
    toast.warning(description, {
        position: toast.POSITION.TOP_CENTER,
        autoClose: 4000,
        hideProgressBar: false,
        transition: Flip,
        theme: "colored"
    });
}

const InfoToast = (description: string | undefined) => {
    toast.info(description, {
        position: toast.POSITION.TOP_CENTER,
        autoClose: 4000,
        hideProgressBar: false,
        transition: Flip,
        theme: "colored"
    });
}

const SuccessToast = (description: string | undefined) => {
    toast.success(description, {
        position: toast.POSITION.TOP_CENTER,
        autoClose: 4000,
        hideProgressBar: false,
        transition: Flip,
        theme: "colored"
    });
}

const ErrorToast = (description: string | undefined) => {
    toast.error(description, {
        position: toast.POSITION.TOP_CENTER,
        autoClose: 4000,
        hideProgressBar: false,
        transition: Flip,
        theme: "colored"
    });
}

const ToastShow = (type: number | undefined, description: string | undefined) => {
    switch (type) {
        case 0: {
            ErrorToast(description)
            break;
        }
        case 1: {
            SuccessToast(description)
            break;
        }
        case 2: {
            WarningToast(description)
            break;
        }
        case 3: {
            WarningToast(description)
            break;
        }
    }
}

const ToastHelper = {
    WarningToast,
    InfoToast,
    SuccessToast,
    ErrorToast,
    ToastShow
}

export default ToastHelper;