import { observer } from "mobx-react-lite";
import { Button, Divider, Form, Header, Icon, Label, Message, Segment } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { useHistory, useParams } from 'react-router-dom';
import FileUploadWidget from "../../app/common/fileUpload/FileUploadWidget";
import { SemanticToastContainer } from "react-semantic-toasts";
import 'react-semantic-toasts/styles/react-semantic-alert.css';
import "../file/style.css";
import SemanticDatepicker from "react-semantic-ui-datepickers";

export default observer(function FileUploadForm() {

    const history = useHistory();
    const { fileStore } = useStore();
    const { uploadMb51, uploadZm20, uploadZs14, uploading, createReport, handleDateChange, reportButtonDisabled,reportDate } = fileStore;
    const { id } = useParams<{ id: string }>();
    return (
        <Segment color="red" clearing>
            <Header content="BMI Dosya Yükleme" size="huge" sub color="black" />

            <Message floating info>
                <Message.Header>Bilgilendirme</Message.Header>
                <p>
                    - Yükleme yapmadan önce "Rapor Tarihi" seçiniz.<br></br>
                    - Dosyaları yükleyiniz.<br></br>
                    - Yüklemeleriniz tamamlandıktan sonra "Rapor Oluştur" butonuna tıklayıp raporu oluşturunuz.
                </p>
            </Message>
            <Message warning>
                <Message.Header>Uyarı</Message.Header>
                <p>
                    Eskiye yönelik yüklemelerde daha önceden o tarih için ZM20 ve MB51 dataları var ise eğer silinip bu yüklemeniz geçerli sayılacaktır!
                </p>
            </Message>

            <Divider horizontal>
                <Header as='h4'>
                    <Icon name="add to calendar" />
                </Header>
            </Divider>
            <br></br>

            <Form.Field>
                <SemanticDatepicker
                    value={reportDate}
                    format="DD/MM/YYYY"
                    locale="tr-TR"
                    type="basic"
                    placeholder="Rapor Tarihi"
                    onChange={(event, data) => {handleDateChange(data.value) }}
                    maxDate={new Date()} />
                <Label pointing="left" prompt>
                    Rapor Tarihi Giriniz
                </Label>
            </Form.Field>


            <Button as='div' style={{ marginTop: "3em" }} labelPosition='right'>
                <Button disabled={false} loading={uploading} onClick={() => createReport()} basic color='green' >
                    <Icon name='file' />
                    Rapor Oluştur
                </Button>
            </Button>

            <Divider horizontal>
                <Header as='h4'>
                    <Icon name="upload" />

                </Header>
            </Divider>


            <Segment.Group piled>
                <Segment>
                    <FileUploadWidget content="ZM20" uploadFile={uploadZm20} loading={uploading} />
                </Segment>
                <Segment>
                    <FileUploadWidget content="ZS14" uploadFile={uploadZs14} loading={uploading} />
                </Segment>
                <Segment>
                    <FileUploadWidget content="MB51" uploadFile={uploadMb51} loading={uploading} />
                </Segment>


            </Segment.Group>

            <SemanticToastContainer position="top-right" />


        </Segment>
    )
})