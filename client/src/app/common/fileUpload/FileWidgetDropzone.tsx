import React, { useCallback } from 'react'
import { useDropzone } from 'react-dropzone'
import ToastHelper from '../../helpers/ToastHelper';
import { toast, ToastOptions } from 'react-semantic-toasts';
import { Header, Icon } from 'semantic-ui-react';

interface Props {
    setFiles: (files: any) => void;
    content: string;
}

export default function FileWidgetDropzone({ setFiles, content }: Props) {
    const dropZoneStyles = {
        border: "dashed 3px #eee",
        borderColor: "#eee",
        borderRadius: "5px",
        paddingTop: "30px",
        textAlign: "center" as "center",
        height: 150
    };

    const dropZoneActive = {
        borderColor: "green",
    };

    const onDrop = useCallback(acceptedFiles => {
        if (acceptedFiles[0].type == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {
            setFiles(acceptedFiles.map((file: any) => Object.assign(file, {
                preview: URL.createObjectURL(file)
            })))
        } else {
            ToastHelper.WarningToast("Lütfen excel dosya seçiniz")
        }
    }, [setFiles])

    const { getRootProps, getInputProps, isDragActive } = useDropzone({ onDrop })

    return (
        <div {...getRootProps()} style={isDragActive ? { ...dropZoneStyles, ...dropZoneActive } : dropZoneStyles}>
            <input {...getInputProps()} />
            <Icon name='upload' size='huge' />
            <Header content={content} />
        </div>
    )
}