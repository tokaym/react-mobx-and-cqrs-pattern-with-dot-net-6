import { observer } from "mobx-react-lite";
import { Button, Divider, Form, Header, Icon, Label, Message, Segment } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { useHistory, useParams } from 'react-router-dom';
import FileUploadWidget from "../../app/common/fileUpload/FileUploadWidget";
import { SemanticToastContainer } from "react-semantic-toasts";
import 'react-semantic-toasts/styles/react-semantic-alert.css';
import "../file/style.css";
import SemanticDatepicker from "react-semantic-ui-datepickers";

export default observer(function RomaniaFileUploadForm() {

    const history = useHistory();
    const { romaniaFileStore } = useStore();
    const { uploadRomaniaZm20, uploading, createReport, handleDateChange, reportButtonDisabled,reportDate } = romaniaFileStore;
    const { id } = useParams<{ id: string }>();
    return (
        <Segment color="red" clearing>
            <Header content="Romanya Dosya Yükleme" size="huge" sub color="black" />
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
                    <FileUploadWidget content="ZM20" uploadFile={uploadRomaniaZm20} loading={uploading} />
                </Segment>
            </Segment.Group>

            <SemanticToastContainer position="top-right" />


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
                    Eskiye yönelik yüklemelerde daha önceden o tarih için Romanya ZM20 dataları var ise eğer silinip bu yüklemeniz geçerli sayılacaktır!
                </p>
            </Message>


        </Segment>
    )
})