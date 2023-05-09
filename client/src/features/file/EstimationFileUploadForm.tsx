import { observer } from "mobx-react-lite";
import { Button, Divider, Form, Header, Icon, Label, Message, Segment } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { useHistory, useParams } from 'react-router-dom';
import FileUploadWidget from "../../app/common/fileUpload/FileUploadWidget";
import { SemanticToastContainer } from "react-semantic-toasts";
import 'react-semantic-toasts/styles/react-semantic-alert.css';
import "../file/style.css";
import SemanticDatepicker from "react-semantic-ui-datepickers";

export default observer(function EstimationFileUploadForm() {

    const history = useHistory();
    const { estimateUploadStore } = useStore();
    const { uploadEstimate, uploading } = estimateUploadStore;
    const { id } = useParams<{ id: string }>();
    return (
        <Segment color="red" clearing>
            <Header content="Dosya Yükleme" size="huge" sub color="black" />

            <Message floating info>
                <Message.Header>Bilgilendirme</Message.Header>
                <p>
                    - Tahminlemenin çalışması için buradan dosyanızı yükleyiniz.<br></br>
                </p>
            </Message>
            <br></br>

            <Divider horizontal>
                <Header as='h4'>
                    <Icon name="upload" />

                </Header>
            </Divider>


            <Segment.Group piled>
                <Segment>
                    <FileUploadWidget content="Forcast" uploadFile={uploadEstimate} loading={uploading} />
                </Segment>
            </Segment.Group>

            <SemanticToastContainer position="top-right" />


        </Segment>
    )
})