import React, { useState } from "react";
import { Button, Grid, Header, Icon } from "semantic-ui-react";
import { useEffect } from 'react';
import { Cropper } from "react-cropper";
import "cropperjs/dist/cropper.css";
import FileWidgetDropzone from "./FileWidgetDropzone";
import LoadingComponent from "../../layout/LoadingComponents";
import { toast } from "react-semantic-toasts";
import ToastHelper from "../../helpers/ToastHelper";

interface Props {
    loading: boolean,
    uploadFile: (file: Blob) => void,
    content: string
}


export default function FileUploadWidget({ loading, uploadFile, content}: Props) {
    const [files, setFiles] = useState<any>([]);
    const [cropper, setCropper] = useState<Cropper>();
    let isLoad : boolean = false;
    async function onCrop() {
        loading = true;
        let response = await uploadFile(files[0]);
        let responseString : Boolean= new Boolean(response);
        if(responseString.valueOf() === true){
            ToastHelper.SuccessToast(content + " dosyası başarılı şekilde yüklendi");
            isLoad = false;
        }
    }

    useEffect(() => {
        return () => {
            files.forEach((file: any) => {
                URL.revokeObjectURL(file.preview)
            });
        }
    }, [files])

    return (
        <Grid>
            <Grid.Column width={4}>
                <Header sub color="black" content={content + " Yükle"} />
                <FileWidgetDropzone setFiles={setFiles} content={content} />
            </Grid.Column>
            <Grid.Column width={1} />
            <Grid.Column width={4}>
                <Header sub color="black" content="Dosya" />
                {files && files.length > 0 &&
                    <>
                        <div style={{ marginTop: 35 }}>
                            <Icon name='file excel' size='huge' />
                            <Header content={files[0].name} />
                        </div>
                    </>
                }
            </Grid.Column>
            <Grid.Column width={1} />
            <Grid.Column width={4}>
                <Header sub color="black" content="Upload" />
                {files && files.length > 0 &&
                    <>
                        <div className="img-preview" style={{ minHeight: 50, overflow: "hidden" }} />
                        <Button.Group widths={4}>
                            <Button loading={loading} disabled={loading} onClick={onCrop} positive icon="check" />
                            <Button disabled={loading} onClick={() => setFiles([])} icon="close" />
                        </Button.Group>
                    </>
                }
            </Grid.Column>
        </Grid >
    )
}